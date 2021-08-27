using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace flash
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
                throw new FlashException("Array 'creations' cannot be null or empty");
        }

        public string Name { get; }
        public IEnumerable<Creation> Creations => _creations;
        public IEnumerable<Variable> Variables => _variables;

        public async Task Create()
        {
            foreach (var creation in Creations)
            {
                if (creation.HasFile)
                {
                    var path = ReplaceVariables(creation.WritingPath);
                    var content = ReplaceVariables(await creation.GetFileContent());

                    if (creation.HasFolder)
                    {
                        var folder = ReplaceVariables(creation.Folder);
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                    }
                    
                    await File.WriteAllTextAsync(path, content);
                }
                else
                {
                    var path = ReplaceVariables(creation.Folder);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                }
            }
        }

        private string ReplaceVariables(string content)
        {
            foreach (var variable in Variables)
            {
                content = content.Replace(variable.Replace, variable.Value);
            }

            return content;
        }
    }
}