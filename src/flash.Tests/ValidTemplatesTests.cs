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
        public async Task ValidCreationsTest()
        {
            await ValidTemplateTest("validCreations");
        }
        
        [Fact]
        public async Task ValidVariablesTest()
        {
            await ValidTemplateTest("validVariables");
        }

        private async Task ValidTemplateTest(string location)
        {
            var templateFolder = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "ValidTemplates",
                location);
            
            var templates = new FlashTemplates(templateFolder);
            await templates.Load();
            
            Assert.True(templates.IsValid);
        }
    }
}