using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Utility;
using System.IO;
using ECom.Infrastructure;
using System.Configuration;
using ECom.Domain;

namespace ECom.Console
{
    class Program
    {
        static void Main(string[] args)
        {
			bool continueLooping = true;
			while (continueLooping)
			{
				System.Console.Write("Enter command: ");

				try
				{
					string command = System.Console.ReadLine();

					continueLooping = ProcessCommand(command);
				}
				catch (Exception ex)
				{
					System.Console.WriteLine("Something happened:");
					System.Console.WriteLine(ex.ToString());
				}
			}
        }

		private static bool ProcessCommand(string command)
		{
			bool continueLooping = true;

			if (command == "help")
			{
				System.Console.WriteLine("Possible commands:");
                System.Console.WriteLine("rebuild_read_model");
				System.Console.WriteLine("exit");
			}
			else if (command == "rebuild_read_model")
			{
				System.Console.WriteLine("Starting read model rebuild process...");
                ReadModelRebuilder.Rebuild(ConfigurationManager.AppSettings["REDISCLOUD_URL_STRIPPED"]);
				System.Console.WriteLine("Finished rebuilding read model.");
			}
			else if (command == "exit")
			{
				continueLooping = false;

				System.Console.WriteLine("Exiting. Please any key to close...");
				System.Console.Read();
			}
			else
			{
				System.Console.WriteLine("Command not recognised. Type 'help' for the list of allowed commands.");
			}


			return continueLooping;
		}
    }
}
