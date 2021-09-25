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
        public async Task ValidTemplateTest()
        {
            await ValidTest("validTemplate");
        }
        
        [Fact]
        public async Task ValidTemplateNoConfigTest()
        {
            await ValidTest("validTemplateNoConfig");
        }
        
        [Fact]
        public async Task ValidTemplateVariablesTest()
        {
            await ValidTest("validTemplateVariables");
        }

        private async Task ValidTest(string location)
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