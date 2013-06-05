using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Bus;
using System.Reflection;
using ECom.Utility;
using SubSonic.Repository;
using SubSonic.DataProviders;
using ECom.ReadModel;
using SubSonic.Query;
using System.IO;
using ECom.Messages;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Mono.Cecil;
using ECom.ReadModel.Views;

namespace ECom.Infrastructure
{
	/// <summary>
	/// Rebuilds read model from events 
	/// </summary>
	public static class ReadModelRebuilder
	{
		public static void Rebuild(string redisDbUri)
		{
            Argument.ExpectNotNullOrWhiteSpace(() => redisDbUri);

			var readModelAssembly = Assembly.Load(new AssemblyName("ECom.ReadModel"));
			var bus = new Bus.Bus(false);
            var eventStore = new EventStore.Redis.EventStore(redisDbUri, bus);

			var dtoManager = new RedisDtoManager(redisDbUri);
			var oldProjections = dtoManager.Get<Projections>("all");
			if (oldProjections == null)
			{
				oldProjections = new Projections();
			}

			//find all projection types
			var projections = readModelAssembly.GetTypes()
											.Where(t => !t.IsAbstract
												&& t.GetInterfaces()
														.Any(i => i.IsGenericType
																&& i.GetGenericTypeDefinition() == typeof(IProjection<>)))
											.Select(t => new {
												Name = t.Name,
												ProjectionType = t,
												DtoTypes = t.GetInterfaces()
															.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IProjection<>))
															.Select(i => i.GetGenericArguments().First())
											});

			//compute hash for each projection and its DTOs
			var projectionInfos = projections.Select((p, i) => {
				var projectionHash =
					GetClassHash(p.ProjectionType) +
					"\r\n " + String.Join("", p.DtoTypes.Select(d => "\r\n " + GetClassHash(d)));

				return new { 
					Name = p.Name,
					ProjectionType = p.ProjectionType,
					DtoTypes = p.DtoTypes,
					Hash = projectionHash,
					NeedsRebuild = !oldProjections.Items.ContainsKey(p.Name) 
									|| oldProjections.Items[p.Name].Hash != projectionHash
				};
			});

			//projections wich no longer exist
			var obsoleteProjections = oldProjections.Items.Where(kvp => !projectionInfos.Any(p => p.Name == kvp.Key))
															.Select(kvp => kvp.Value)
															.ToArray();
			foreach (var projection in obsoleteProjections)
			{
				//drop obsolete dtos
				foreach (var dtoType in projection.DtoTypes)
				{
					DeleteDtos(dtoType, dtoManager);
				}
			}

			var projectionsToRebuild = projectionInfos.Where(p => p.NeedsRebuild).ToArray();
			if (!projectionsToRebuild.Any())
			{
				return;
			}

			foreach (var projection in projectionsToRebuild)
			{
				//drop dtos requiring rebuild
				foreach (var dtoType in projection.DtoTypes)
				{
					DeleteDtos(dtoType, dtoManager);
				}

				MessageHandlersRegister.RegisterEventHandlers(new[] { projection.ProjectionType }, bus, dtoManager);
			}

			//republish all events to registered projections requiring rebuild
            var allEvents = eventStore.GetAllEvents();
            foreach (var e in allEvents)
            {
                bus.Publish(e);
            }

			//save updated projections with hashes
			var newProjectionInfos = new Dictionary<string, ProjectionInfo>();
			projectionInfos.ToList()
							.ForEach(p => newProjectionInfos
											.Add(p.Name, new ProjectionInfo { 
												Name = p.Name,
												Hash = p.Hash,
												DtoTypes = p.DtoTypes.ToList()
											}));
			var newProjections = new Projections 
			{
				Items = newProjectionInfos
			};

			dtoManager.Add<Projections>("all", newProjections);
		}

        [Serializable]
        public class Projections : Dto
        {
			public Dictionary<string, ProjectionInfo> Items { get; set; }

			public Projections()
			{
				Items = new Dictionary<string, ProjectionInfo>();
			}
        }

        [Serializable]
        public class ProjectionInfo : Dto
        {
            public string Name { get; set; }
			public List<Type> DtoTypes { get; set; }
            public string Hash { get; set; }
        }

		private static void DeleteDtos(Type dtoType, IDtoManager manager)
		{
			MethodInfo genericDeleteAll = manager.GetType().GetMethod("DeleteAll").MakeGenericMethod(new[] { dtoType });
			genericDeleteAll.Invoke(manager, new object[0]);
		}

        private static string GetClassHash(Type type)
        {
            var location = type.Assembly.Location;
            var mod = ModuleDefinition.ReadModule(location);
            var builder = new StringBuilder();

            var typeDefinition = mod.GetType(type.FullName);
            builder.AppendLine(typeDefinition.Name);
            ProcessMembers(builder, typeDefinition);

            // we include nested types
            foreach (var nested in typeDefinition.NestedTypes)
            {
                ProcessMembers(builder, nested);
            }

            return builder.ToString();
        }

        private static void ProcessMembers(StringBuilder builder, TypeDefinition typeDefinition)
        {
            foreach (var md in typeDefinition.Methods.OrderBy(m => m.ToString()))
            {
                builder.AppendLine("  " + md);

                foreach (var instruction in md.Body.Instructions)
                {
                    // we don't care about offsets
                    instruction.Offset = 0;
                    builder.AppendLine("    " + instruction);
                }
            }

            foreach (var field in typeDefinition.Fields.OrderBy(f => f.ToString()))
            {
                builder.AppendLine("  " + field);
            }
        }
	}
}
