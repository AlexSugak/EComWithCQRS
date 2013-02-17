using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageContracts;
using System.Runtime.CompilerServices;
using System.CodeDom.Compiler;
using System.Globalization;

namespace ECom.Dsl
{
	public class CommandsEventsGenerator : IGenerateCode
	{
		public CommandsEventsGenerator()
		{
			Flavor = MemberFlavor.ReadOnlyAutoProperty;

            this.NullIdType = "NullId";
			this.EventInterface = "IEvent<{0}>";
			this.CommandInterface = "ICommand<{0}>";
            this.FunctionalCommandInterface = "IFunctionalCommand";
		}

		public void Generate(Context context, IndentedTextWriter writer)
		{
			foreach (Contract contract in context.Contracts)
			{
				var firstArgInId = contract.Members.First().Type.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase);

				writer.WriteLine();
				//writer.WriteLine("[ProtoContract]");
				writer.WriteLine("[Serializable]");
				writer.WriteLine("[GeneratedCodeAttribute(\"MessagesGenerator\", \"1.0.0.0\")]");
				writer.Write("public sealed class {0}", contract.Name);
				List<string> interfaces = new List<string>();

				if ((contract.Modifier & ContractModifier.CommandInterface) == ContractModifier.CommandInterface)
				{
					if (firstArgInId)
					{
						interfaces.Add(String.Format(CultureInfo.InvariantCulture, this.CommandInterface, contract.Members.First().Type));
					}
					else
					{
						interfaces.Add(this.FunctionalCommandInterface);
					}
				}
				if ((contract.Modifier & ContractModifier.EventInterface) == ContractModifier.EventInterface)
				{
					interfaces.Add(String.Format(CultureInfo.InvariantCulture, this.EventInterface, contract.Members.First().Type));
				}
				if (interfaces.Any<string>())
				{
					writer.Write(" : {0}", string.Join(", ", interfaces.ToArray()));
				}
				writer.WriteLine();
				writer.WriteLine("{");
				writer.Indent++;

				if (firstArgInId)
				{
					writer.Write("public {0} ", contract.Members.First().Type);
					writer.WriteLine("Id { get; set; }");
				}

				writer.WriteLine("public int Version { get; set; }");
                writer.WriteLine("public DateTime Date { get; set; }");

				if (contract.Members.Count > 0)
				{
					this.WriteMembers(contract, writer);

					writer.Write("public {0} () ", contract.Name);
					writer.Write("{");
					writer.WriteLine("}");

					writer.Write("public {0} (", contract.Name);
					this.WriteParameters(contract, writer);
					writer.WriteLine(")");

					writer.WriteLine("{");
					writer.Indent++;
					this.WriteAssignments(contract, writer);
					writer.Indent--;
					writer.WriteLine("}");
				}

				WriteEqualsOverride(contract, writer);
				WriteGetHashCodeMethod(contract, writer);
				WriteEqualOperatorOverride(contract, writer);

				writer.Indent--;
				writer.WriteLine("}");
			}
		}

		private void WriteAssignments(Contract contract, IndentedTextWriter writer)
		{
			bool first = true;

			foreach (Member member in contract.Members)
			{
				string name = member.Name;

				if (first && member.Type.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
				{
					name = "Id";
				}

				first = false;

				writer.WriteLine("{0} = {1};", name, GeneratorUtil.ParameterCase(member.Name));
			}
		}

		private void WriteMembers(Contract contract, IndentedTextWriter writer)
		{
			bool first = true;
			foreach (Member member in contract.Members)
			{
				if (!(first && member.Type.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase)))
				{
					switch (this.Flavor)
					{
						case MemberFlavor.ReadOnlyField:
							//writer.WriteLine("[ProtoMember({0})] public readonly {1} {2};", idx, member.Type, member.Name);
							writer.WriteLine("public {0} {1};", member.Type, member.Name);
							break;

						case MemberFlavor.ReadOnlyAutoProperty:
							//writer.WriteLine("[ProtoMember({0})]", idx);
							writer.Write("public {0} {1} ", member.Type, member.Name);
							writer.WriteLine("{ get; set; }");
							break;

						default:
							throw new ArgumentOutOfRangeException();
					}
				}

				first = false;
			}
		}

		private void WriteFirstParameter(Contract contract, IndentedTextWriter writer)
		{
			writer.Write("{0} {1}", contract.Members.First().Type, GeneratorUtil.ParameterCase(contract.Members.First().Name));
		}

		private void WriteParameters(Contract contract, IndentedTextWriter writer)
		{
			bool first = true;
			foreach (Member member in contract.Members)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					writer.Write(", ");
				}
				writer.Write("{0} {1}", member.Type, GeneratorUtil.ParameterCase(member.Name));
			}
		}

		private void WriteEqualOperatorOverride(Contract contract, IndentedTextWriter writer)
		{
			writer.WriteLine("public static bool operator ==({0} a, {0} b)", contract.Name);
			writer.WriteLine("{");
			writer.Indent++;

			writer.WriteLine("if (System.Object.ReferenceEquals(a, b))");
			writer.WriteLine("{");
			writer.Indent++;
			writer.WriteLine("return true;");
			writer.Indent--;
			writer.WriteLine("}");

			writer.WriteLine("if (((object)a == null) || ((object)b == null))");
			writer.WriteLine("{");
			writer.Indent++;
			writer.WriteLine("return false;");
			writer.Indent--;
			writer.WriteLine("}");


			writer.WriteLine("return a.Equals(b);");
			writer.Indent--;
			writer.WriteLine("}");

			writer.WriteLine("public static bool operator !=({0} a, {0} b)", contract.Name);
			writer.WriteLine("{");
			writer.Indent++;
			writer.WriteLine("return !(a == b);");
			writer.Indent--;
			writer.WriteLine("}");
		}

		private void WriteEqualsOverride(Contract contract, IndentedTextWriter writer)
		{
			writer.WriteLine("public override bool Equals(object obj)");
			writer.WriteLine("{");
			writer.Indent++;

			writer.WriteLine("if (obj == null)");
			writer.WriteLine("{");
			writer.Indent++;
			writer.WriteLine("return false;");
			writer.Indent--;
			writer.WriteLine("}");

			writer.WriteLine("var target = obj as {0};", contract.Name);
			writer.WriteLine("if (target == null)");
			writer.WriteLine("{");
			writer.Indent++;
			writer.WriteLine("return false;");
			writer.Indent--;
			writer.WriteLine("}");

			writer.Write("return ");
			bool first = true;
			foreach (Member member in contract.Members)
			{
				string name = member.Name;

				if (first && member.Type.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
				{
					name = "Id";
				}

				if (!first)
				{
					writer.Write(" && ");
				}

				writer.Write("{0} == target.{0}", name);

				first = false;
			}
			writer.WriteLine(";");

			writer.Indent--;
			writer.WriteLine("}");
		}

		private void WriteGetHashCodeMethod(Contract contract, IndentedTextWriter writer)
		{
			writer.WriteLine("public override int GetHashCode()");
			writer.WriteLine("{");
			writer.Indent++;

			writer.Write("return ");
			bool first = true;

			foreach (Member member in contract.Members)
			{
				string name = member.Name;

				if (first && member.Type.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
				{
					name = "Id";
				}

				if (!first)
				{
					writer.Write(" ^ ");
				}

				writer.Write("{0}.GetHashCode()", name);

				first = false;
			}
			writer.WriteLine(";");

			writer.Indent--;
			writer.WriteLine("}");
		}

		public string NullIdType { get; set; }
		public string CommandInterface { get; set; }
		public string FunctionalCommandInterface { get; set; }
		public string EventInterface { get; set; }
		public MemberFlavor Flavor { get; set; }
	}
}
