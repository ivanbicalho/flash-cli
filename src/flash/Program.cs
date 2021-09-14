using System;
using System.Threading.Tasks;
using flash.Commands;
using LightCli;
using LightCli.Commands;

namespace flash
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var runner = new CliRunner();

            runner
                .AddCommand(new NewCommand())
                .AddCommand(new ValidateCommand())
                .AddVersionCommand();

            var result = await runner.Run(args);
            
            if (result.Success)
                return;
            
            Console.WriteLine(result.Message);
            
            if (result.Command == null)
            {
                ShowAvailableCommandsMessage(runner);
                return;
            }

            result.Command.ShowDefaultHelp();
        }

        private static void ShowAvailableCommandsMessage(CliRunner runner)
        {
            runner.ShowDefaultAvailableCommandsMessage();
            Console.WriteLine();
            Console.WriteLine("Questions? See the docs: https://github.com/ivanbicalho/flash-cli");
        }
    }
}
