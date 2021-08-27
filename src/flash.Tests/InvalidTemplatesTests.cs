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
            var templates = await InvalidTemplateTest("noFlashTemplatesFolder");
            Assert.False(templates.IsValid);
            Assert.Equal("missed_flash_folder", templates.ErrorCode);
        }
        
        [Fact]
        public async Task NoTemplatesTest()
        {
            var templates = await InvalidTemplateTest("noTemplates");
            Assert.False(templates.IsValid);
            Assert.Equal("missed_templates", templates.ErrorCode);
        }

        [Fact]
        public async Task NonExistentFileTest()
        {
            var templates = await InvalidTemplateTest("nonExistentFile");
            Assert.False(templates.IsValid);
            Assert.Equal("invalid_creation_missed_file", templates.ErrorCode);
        }
        
        [Fact]
        public async Task InvalidConfigFormatTest()
        {
            var templates = await InvalidTemplateTest("invalidConfigFormat");
            Assert.False(templates.IsValid);
            Assert.Equal("invalid_config_json", templates.ErrorCode);
        }
        
        [Fact]
        public async Task NoConfigFormatTest()
        {
            var templates = await InvalidTemplateTest("noConfigFormat");
            Assert.False(templates.IsValid);
            Assert.Equal("invalid_config_json", templates.ErrorCode);
        }
        
        [Fact]
        public async Task EmptyCreationsTest()
        {
            var templates = await InvalidTemplateTest("emptyCreations");
            Assert.False(templates.IsValid);
            Assert.Equal("invalid_creations_empty", templates.ErrorCode);
        }

        private async Task<FlashTemplates> InvalidTemplateTest(string location)
        {
            var templateFolder = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "InvalidTemplates",
                location);
            
            var templates = new FlashTemplates(templateFolder);
            await templates.Load();

            return templates;
        }
        
        // [Fact]
        // public void SouldCreateFolder()
        // {
        // }
    }
}