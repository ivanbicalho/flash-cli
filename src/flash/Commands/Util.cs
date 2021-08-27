using System.IO;
using System.Reflection;

namespace flash.Commands
{
    public static class Util
    {
        public static readonly string DefaultLocationFlashTemplatesFolder =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "flash-templates");
    }
}