using System;
using LightCli.Args;
using LightCli.Attributes;

namespace flash.Models
{
    public class NewCommandArgs : IArgs
    {
        [IndexArg(0, description: "Template name")]
        public string TemplateName { get; set; }

        // public Args(string[] args)
        // {
        //     if (args == null || args.Length < 2)
        //     {
        //         ErrorMessage = "Invalid arguments";
        //         return;
        //     }
        //
        //     if (args[0] != "new")
        //     {
        //         ErrorMessage = "Invalid command";
        //         return;
        //     }
        //
        //     if (string.IsNullOrWhiteSpace(args[2]))
        //     {
        //         ErrorMessage = "Template name cannot be null";
        //         return;
        //     }
        //
        //     IsValid = true;
        //     TemplateName = args[2];
        // }
        //
        // public bool IsValid { get; }
        // public string TemplateName { get; }
        // public string ErrorMessage { get; }
        // public string UsageMessage => "Usage: flash new template-name";
    }
}