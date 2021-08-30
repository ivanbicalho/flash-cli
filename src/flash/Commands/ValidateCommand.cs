using System;
using System.Threading.Tasks;
using flash.Domain;
using LightCli.Args;
using LightCli.Commands;

namespace flash.Commands
{
    public class ValidateCommand : Command<NoArgs>
    {
        public override string CommandName => "validate";
        public override string Description => "Validate the flash-templates folder configuration";
        public override string ExampleUsage => $"flash {CommandName}";
        
        protected override async Task Run(NoArgs args)
        {
            var templates = new FlashTemplates();
            await templates.Load();

            if (templates.IsValid)
                Console.WriteLine("Templates configuration are validated successfully!");
            else
                Console.WriteLine(templates.ErrorMessage);
        }
    }
}