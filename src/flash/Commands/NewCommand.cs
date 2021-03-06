using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using flash.Domain;
using LightCli.Commands;

namespace flash.Commands
{
    public class NewCommand : Command<NewCommandArgs>
    {
        public override string CommandName => "new";
        public override string Description => "Creates new folder/files based on a template";
        public override string ExampleUsage => $"flash {CommandName} template-name";

        protected override async Task Run(NewCommandArgs args)
        {
            var templates = new FlashTemplates();
            await templates.Load();

            if (!templates.IsValid)
            {
                Console.WriteLine(templates.ErrorMessage);
                return;
            }

            var template = templates.Get(args.TemplateName);
            if (template == null)
            {
                Console.WriteLine("Invalid template name");
                Console.WriteLine("Available templates:");

                foreach (var t in templates.Templates)
                {
                    Console.Write($"   flash new {t.Name}");
                    if (t.HasDescription)
                        Console.Write($": {t.Description}");
                    Console.WriteLine();
                }

                return;
            }

            ReadVariables(template);
            await template.Create();

            Console.WriteLine("Template created successfully!");
        }

        private static void ReadVariables(Template template)
        {
            foreach (var variable in template.Variables)
            {
                do
                {
                    Console.Write($"{variable.Question} ");
                    variable.Value = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(variable.Value))
                        Console.WriteLine("Value cannot be empty, please enter again");
                } while (string.IsNullOrWhiteSpace(variable.Value));
            }
        }
    }
}