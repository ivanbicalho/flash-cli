using System;
using System.IO;
using System.Linq;
using System.Reflection;
using flash.Domain;

namespace flash.Tests.Fixtures
{
    public class CreateTemplateFixture : IDisposable
    {
        public CreateTemplateFixture()
        {
            var templateFolder = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "ValidTemplates",
                "validTemplateVariables");
            
            var templates = new FlashTemplates(templateFolder); 
            templates.Load().Wait();
            
            var template = templates.Get("use-case");
            template.Variables.First().Value = "NewTest";
            template.Create().Wait();
        }

        public void Dispose()
        {
            File.Delete("MyFile.txt");
            File.Delete("NewTest2.txt");
            File.Delete("Variables.txt");
            
            Directory.Delete("AnotherFolder", true);
            Directory.Delete("Folder", true);
            Directory.Delete("NEWTEST", true);
        }
    }
}