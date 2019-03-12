using CA.Commands;
using CA.Interfaces;
using CA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace CA
{
    class Program
    {
        /// <summary>
        /// main loop consists of registering commands
        /// selection of command
        /// execution of command
        /// loop back until exitcommand
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please make a selection of task by number");
                Console.WriteLine("-----------------------------------------");
                var commands = new ICommand[]
                {
                    new PostCommand(),
                    new GetCommand(),
                    new UpdateCommand(),
                    new DeleteCommand(),
                    new ExitCommand()
                };

                foreach (var command in commands)
                {
                    Console.WriteLine("{0}", command.Description);
                }

                var choice = string.Empty;
                var commandIndex = -1;
                do
                {
                    choice = Console.ReadLine();
                } while (!int.TryParse(choice, out commandIndex) || commandIndex > commands.Length);

                var data = commands[commandIndex - 1].GatherData();
                var response = commands[commandIndex - 1].Execute(data).Result;
                PrintResponse(response);
            }
        }

        static void PrintResponse(ResponseModel model)
        {
            if(model == null)
            {
                Console.WriteLine("User Deleted");
                return;
            }
            if(model.ValidationResults.Count == 0)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("Output:");
                Console.WriteLine("----------------------------");
                Console.WriteLine("{");
                Console.WriteLine("   guid: {0}", model.CredentialModel.UserId);
                Console.WriteLine("   expire: {0}", model.CredentialModel.Expire);
                Console.WriteLine("   user: {0}", model.CredentialModel.Username);
                Console.WriteLine("}");
            }
            else if(model.ValidationResults.Count > 0)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("Errors:");
                Console.WriteLine("----------------------------");
                Console.WriteLine("{");
                foreach (var error in model.ValidationResults)
                {
                    Console.WriteLine("   Validation Result: {0}", error);
                }
                Console.WriteLine("}");
            }
        }
    }

    
}
