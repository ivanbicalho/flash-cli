using System.IO;
using System.Threading.Tasks;
using flash.Tests.Fixtures;
using Xunit;

namespace flash.Tests
{
    public class ValidCreateTemplateTests : IClassFixture<CreateTemplateFixture>
    {
        [Fact]
        public async Task ValidateRootFileNewTestTest()
        {
            Assert.True(File.Exists("NewTest2.txt"));

            var content = await File.ReadAllTextAsync("NewTest2.txt");
            Assert.Equal("test2 is different from NewTest2", content);
        }
        
        [Fact]
        public async Task ValidateRootFileMyFileTest()
        {
            Assert.True(File.Exists("MyFile.txt"));
            
            var content = await File.ReadAllTextAsync("MyFile.txt");
            Assert.Equal("just my file", content);
        }
        
        [Fact]
        public async Task ValidateRootFileVariablesTest()
        {
            Assert.True(File.Exists("Variables.txt"));
            
            var content = await File.ReadAllTextAsync("Variables.txt");
            Assert.Equal("variable NewTest, newtest, NEWTEST, newTest, NewTest", content);
        }
        
        [Fact]
        public async Task ValidateNestedAnotherFolderTest()
        {
            Assert.True(Directory.Exists("AnotherFolder"));
            Assert.True(File.Exists("AnotherFolder/NewTest.txt"));
            
            var content = await File.ReadAllTextAsync("AnotherFolder/NewTest.txt");
            Assert.Equal("test is different from NewTest", content);
        }
        
        [Fact]
        public async Task ValidateNestedFolderTest()
        {
            Assert.True(Directory.Exists("Folder/NewTestN2"));
            Assert.True(Directory.Exists("Folder/NewTestN2Empty"));
            Assert.True(File.Exists("Folder/NewTest.txt"));
            Assert.True(File.Exists("Folder/NewTestN2/NewTest2.txt"));

            var content = await File.ReadAllTextAsync("Folder/NewTest.txt");
            Assert.Equal("test is different from NewTest", content);
            
            content = await File.ReadAllTextAsync("Folder/NewTestN2/NewTest2.txt");
            Assert.Equal("test2 is different from NewTest2", content);
        }
        
        [Fact]
        public async Task ValidateFilesLowerUpperCaseTest()
        {
            Assert.True(Directory.Exists("NEWTEST"));
            Assert.True(File.Exists("NEWTEST/newtest.txt"));
            Assert.True(File.Exists("NEWTEST/newTest2.txt"));
            
            var content = await File.ReadAllTextAsync("NEWTEST/newtest.txt");
            Assert.Equal("empty", content);
            
            content = await File.ReadAllTextAsync("NEWTEST/newTest2.txt");
            Assert.Equal("empty", content);
        }
    }
}