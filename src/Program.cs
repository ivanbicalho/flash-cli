using System;
using System.Threading.Tasks;
using flash.Commands;
using flash.Models;
using LightCli;

namespace flash
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var runner = new CliRunner();
            
            runner
                .AddCommand(new NewCommand())
                .AddCommand(new ValidateCommand());

            var result = await runner.Run(args);
            
            if (result.Success)
                return;
            
            Console.WriteLine(result.Message);
            
            if (result.Command == null)
            {
                runner.ShowDefaultAvailableCommandsMessage();
                return;
            }
            
            result.Command.ShowDefaultHelp();
        }
    }
}
