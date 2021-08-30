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
    public class CreateTemplatesTests : IClassFixture<CreateTemplateFixture>
    {
        [Fact]
        public async Task ValidateRootTestFileTest()
        {
            Assert.True(File.Exists("NewTest.txt"));
            
            var content = await File.ReadAllTextAsync("NewTest.txt");
            Assert.Equal("test is different from NewTest", content);
        }
        
        [Fact]
        public async Task ValidateNestedFoldersTest()
        {
            Assert.True(Directory.Exists("MyFolder/NewTest"));
            Assert.True(File.Exists("MyFolder/NewTest/NewTest.txt"));
            
            var content = await File.ReadAllTextAsync("MyFolder/NewTest/NewTest.txt");
            Assert.Equal("test is different from NewTest", content);
        }
        
        [Fact]
        public async Task ValidateFolderTest()
        {
            Assert.True(Directory.Exists("MyFolder"));
            Assert.True(File.Exists("MyFolder/NewTest2.txt"));
            
            var content = await File.ReadAllTextAsync("MyFolder/NewTest2.txt");
            Assert.Equal("test2 is different from NewTest2", content);
        }
        
        [Fact]
        public async Task ValidateFolderLowerCaseTest()
        {
            Assert.True(Directory.Exists("test"));
            Assert.True(File.Exists("test/NewTest2.txt"));
            
            var content = await File.ReadAllTextAsync("test/NewTest2.txt");
            Assert.Equal("test2 is different from NewTest2", content);
        }
        
        [Fact]
        public void ValidateFolderToCreateTest()
        {
            Assert.True(Directory.Exists("FolderToCreate"));
        }
        
        [Fact]
        public async Task ValidateSimpleFileTest()
        {
            Assert.True(File.Exists("MyFile.txt"));
            
            var content = await File.ReadAllTextAsync("MyFile.txt");
            Assert.Equal("just my file", content);
        }
        
        [Fact]
        public async Task ValidateVariablesTest()
        {
            Assert.True(File.Exists("Variables.txt"));
            
            var content = await File.ReadAllTextAsync("Variables.txt");
            Assert.Equal("variable NewTest, newtest, NEWTEST, newTest, NewTest", content);
        }
    }
}