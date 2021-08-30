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
        private List<Creation> _creations = new();
        private List<Variable> _variables = new();

        public Template(string name, string flashTemplatesFolderPath, TemplateModel model)
        {
            Name = name;

            var templateFolderPath = Path.Combine(flashTemplatesFolderPath, name);

            foreach (var creationModel in model.Creations)
                _creations.Add(new Creation(creationModel, templateFolderPath));

            foreach (var variableModel in model.Variables)
                _variables.Add(new Variable(variableModel));

            if (!Creations.Any())
                throw new FlashException("Array 'creations' cannot be null or empty", ErrorCodes.EmptyArrayCreations);
        }

        public string Name { get; }
        public IEnumerable<Creation> Creations => _creations;
        public IEnumerable<Variable> Variables => _variables;

        public async Task Create()
        {
            if (!IsValidVariables())
                throw new FlashException("No variable can be null or empty", ErrorCodes.UnassignedVariables);

            foreach (var creation in Creations)
            {
                if (creation.HasTemplateFile)
                {
                    var path = ReplaceVariables(creation.WritingPath);
                    var content = ReplaceVariables(await creation.GetFileContent());

                    if (creation.HasLocation)
                    {
                        var folder = ReplaceVariables(creation.Location);
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                    }

                    await File.WriteAllTextAsync(path, content);
                }
                else
                {
                    var path = ReplaceVariables(creation.Location);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                }
            }
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