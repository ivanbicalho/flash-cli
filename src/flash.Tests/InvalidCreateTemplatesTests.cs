using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using flash.Commands;
using flash.Domain;
using flash.Domain.Exceptions;
using Xunit;

namespace flash.Tests
{
    public class InvalidCreateTemplatesTests
    {
        [Fact]
        public async Task InvalidAssignVariableTest()
        {
            var templates = await GetValidFlashTemplates("validVariables");
            var template = templates.Get("use-case");

            var ex = await Assert.ThrowsAsync<FlashException>(async () =>
            {
                await template.Create();
            });
            
            Assert.Equal(ErrorCodes.UnassignedVariables, ex.ErrorCode);
        }

        private async Task<FlashTemplates> GetValidFlashTemplates(string location)
        {
            var templateFolder = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "ValidTemplates",
                location);
            
            var templates = new FlashTemplates(templateFolder);
            await templates.Load();

            return templates;
        }
    }
}