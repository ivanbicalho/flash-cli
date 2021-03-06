using LightCli.Args;
using LightCli.Attributes;

namespace flash.Commands
{
    public class NewCommandArgs : IArgs
    {
        [IndexArg(1, description: "Template name", false)]
        public string TemplateName { get; set; }
    }
}