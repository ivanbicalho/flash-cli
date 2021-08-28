using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using flash.Commands;
using Xunit;

namespace flash.Tests
{
    public class InvalidTemplatesTests
    {
        [Fact]
        public async Task NoFlashTemplatesFolderTest()
        {
            await InvalidTemplateTest("noFolder", ErrorCodes.MissingFlashTemplateFolder);
        }

        [Fact]
        public async Task NoTemplatesTest()
        {
            await InvalidTemplateTest("missingTemplates", ErrorCodes.MissingTemplates);
        }

        [Fact]
        public async Task NonExistentFileTest()
        {
            await InvalidTemplateTest("missingCreationFile", ErrorCodes.MissingCreationFile);
        }

        [Fact]
        public async Task InvalidConfigFormatTest()
        {
            await InvalidTemplateTest("invalidConfigFormat", ErrorCodes.InvalidConfigFormat);
        }

        [Fact]
        public async Task NoConfigFormatTest()
        {
            await InvalidTemplateTest("noConfigFormat", ErrorCodes.InvalidConfigFormat);
        }

        [Fact]
        public async Task EmptyCreationsTest()
        {
            await InvalidTemplateTest("emptyCreations", ErrorCodes.EmptyCreations);
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