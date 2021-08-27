using System;
using LightCli.Args;
using LightCli.Attributes;

namespace flash.Models
{
    public class NewCommandArgs : IArgs
    {
        [IndexArg(1, description: "Template name")]
        public string TemplateName { get; set; }
    }
}