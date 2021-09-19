using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using flash.Commands;
using flash.Domain;
using Xunit;

namespace flash.Tests
{
    public class InvalidTemplatesTests
    {
        [Fact]
        public async Task NoFlashTemplatesFolderTest()
        {
            await InvalidTemplateTest("aaa", ErrorCodes.MissingFlashTemplateFolder);
        }

        [Fact]
        public async Task MissingTemplatesTest()
        {
            await InvalidTemplateTest("missingTemplates", ErrorCodes.MissingTemplates);
        }

        [Fact]
        public async Task InvalidConfigFileTest()
        {
            await InvalidTemplateTest("invalidConfigFile", ErrorCodes.InvalidConfigFile);
        }

        [Fact]
        public async Task InvalidQuestionVariableTest()
        {
            await InvalidTemplateTest("invalidQuestionVariable", ErrorCodes.InvalidVariable);
        }
        
        [Fact]
        public async Task InvalidReplaceVariableTest()
        {
            await InvalidTemplateTest("invalidReplaceVariable", ErrorCodes.InvalidVariable);
        }

        private async Task InvalidTemplateTest(string location, string errorCode)
        {
            var templateFolder = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "InvalidTemplates",
                location);

            var templates = new FlashTemplates(templateFolder);
            await templates.Load();

            Assert.False(templates.IsValid);
            Assert.Equal(errorCode, templates.ErrorCode);
        }
    }
}