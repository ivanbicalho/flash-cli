using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using flash.Commands;
using flash.Domain;
using Xunit;

namespace flash.Tests
{
    public class ValidTemplatesTests
    {
        [Fact]
        public async Task NoTemplatesTest1()
        {
            await InvalidTemplateTest("noTemplates");
        }

        private async Task InvalidTemplateTest(string location)
        {
            var templateFolder = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "InvalidTemplates",
                location);
            
            var templates = new FlashTemplates(templateFolder);
            await templates.Load();
            
            Assert.False(templates.IsValid);
        }
        
        // [Fact]
        // public void SouldCreateFolder()
        // {
        // }
    }
}