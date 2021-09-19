using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using flash.Domain.Exceptions;
using flash.Functions;
using flash.Model;

namespace flash.Domain
{
    public class Template
    {
        private List<Variable> _variables = new();

        public Template(string name, string description, string flashTemplatesFolderPath, TemplateModel model)
        {
            Name = name;
            Description = description;
            Directory = Path.Combine(flashTemplatesFolderPath, name);
            
            foreach (var variableModel in model.Variables)
                _variables.Add(new Variable(variableModel));
        }

        public string Directory { get; }
        public string Description { get; }
        public string Name { get; }
        public bool HasDescription => !string.IsNullOrWhiteSpace(Description);
        public IEnumerable<Variable> Variables => _variables;

        public async Task Create()
        {
            if (!IsValidVariables())
                throw new FlashException("No variable can be null or empty", ErrorCodes.UnassignedVariables);

            await CreateAll(string.Empty, Directory);
        }

        private async Task CreateAll(string baseDirectory, string directory)
        {
            var files = System.IO.Directory.GetFiles(directory);
            foreach (var file in files)
            {
                if (IsConfigFile(baseDirectory, file))
                    continue;
                
                var path = ReplaceVariables(GetFilePath(baseDirectory, file));
                var content = ReplaceVariables(await File.ReadAllTextAsync(file));
                await File.WriteAllTextAsync(path, content);
            }
            
            var directories = System.IO.Directory.GetDirectories(directory);
            foreach (var d in directories)
            {
                var path = ReplaceVariables(GetDirectoryPath(baseDirectory, d));
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                
                await CreateAll(path, d);
            }
        }

        private bool IsConfigFile(string baseDirectory, string file)
        {
            return baseDirectory == string.Empty && new FileInfo(file).Name == Consts.ConfigFile;
        }

        private string GetFilePath(string baseDirectory, string file)
        {
            return Path.Combine(baseDirectory, new FileInfo(file).Name);
        }
        
        private string GetDirectoryPath(string baseDirectory, string directory)
        {
            return Path.Combine(baseDirectory, new DirectoryInfo(directory).Name);
        }

        private bool IsValidVariables()
        {
            return Variables.All(v => v.IsValueValid());
        }

        private string ReplaceVariables(string content)
        {
            foreach (var variable in Variables)
            {
                Replace(ref content, variable.Replace, variable.Value);
                ApplyFuncions(ref content, variable.Value);
            }

            return content;
        }

        private void Replace(ref string content, string replace, string value)
        {
            content = content.Replace(replace, value);
        }
        
        private void ApplyFuncions(ref string content, string value)
        {
            foreach (var f in Functions.Functions.List())
            {
                Replace(ref content, f.Search(value), f.Apply(value));
            }
        }
    }
}