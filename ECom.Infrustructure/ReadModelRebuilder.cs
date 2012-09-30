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

namespace ECom.Infrastructure
{
	public static class ReadModelRebuilder
	{
		public static void Rebuild(string readModelConnectionString, string eventStoreConnectionString)
		{
			Argument.ExpectNotNullOrWhiteSpace(() => readModelConnectionString);

			var readModelAssembly = Assembly.Load(new AssemblyName("ECom.ReadModel"));
			var bus = new Bus.Bus(false);
			var eventStore = new EventStore.SQL.EventStore(eventStoreConnectionString, bus);


			//recreate all dto tables
			IDataProvider readModelDbProvider = ProviderFactory.GetProvider(readModelConnectionString, DbClientTypeName.MsSql);
			var dtoTypes = readModelAssembly.GetTypes().Where(t => t != typeof(Dto) && typeof(Dto).IsAssignableFrom(t));
			foreach (var dtoType in dtoTypes)
			{
				//dtop dto tables
				var sql = String.Format(
					@"
declare @cmd varchar(4000)
declare cmds cursor for 
Select
    'drop table [' + Table_Name + ']'
From
    INFORMATION_SCHEMA.TABLES
Where
    Table_Name like '{0}%'

open cmds
while 1=1
begin
    fetch cmds into @cmd
    if @@fetch_status != 0 break
    exec(@cmd)
end
close cmds
deallocate cmds
						", 

					dtoType.Name);

				readModelDbProvider.ExecuteQuery(new QueryCommand(sql, readModelDbProvider));

				//creates table for dto type
				var genericRegister = readModelDbProvider.GetType().GetMethod("MigrateToDatabase").MakeGenericMethod(new[] { dtoType });
				genericRegister.Invoke(readModelDbProvider, new[] { readModelAssembly });
			}

			var readModelRepo = new SimpleRepository(readModelDbProvider, SimpleRepositoryOptions.None);
			var dtoManager = new SubSonicDtoManager(readModelRepo);

			MessageHandlersRegister.RegisterEventHandlers(new [] { readModelAssembly }, bus, dtoManager, new SubSonicReadModelFacade(readModelRepo));

			//republish all events to read model views
			IDataProvider eventStoreDbProvider = ProviderFactory.GetProvider(eventStoreConnectionString, DbClientTypeName.MsSql);
			var formatter = new BinaryFormatter();
			using (var reader = eventStoreDbProvider.ExecuteReader(new QueryCommand("SELECT * FROM [Events] ORDER BY [Date] ASC", eventStoreDbProvider)))
			{
				while (reader.Read())
				{
                    var @event = Deserialize<IEvent>((byte[])reader["Event"], formatter);
					bus.Publish(@event);
				}
			}
		}

		private static TType Deserialize<TType>(byte[] bytes, IFormatter formatter)
		{
			using (var memoryStream = new MemoryStream(bytes))
			{
				return (TType)formatter.Deserialize(memoryStream);
			}
		}
	}
}
