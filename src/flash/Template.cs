using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace flash
{
    public class Template
    {
        public Template(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public string ErrorMessage { get; private set; }
        public bool IsValid => ErrorMessage == null;
        public IEnumerable<Creation> Creations { get; set; }
        public IEnumerable<Variable> Variables { get; set; }

        public async Task<bool> Validate()
        {
            Creations ??= new List<Creation>();
            Variables ??= new List<Variable>();

            var result = await ValidateCreations();
        
            if (result == null)
                result = await ValidateVariables();

            if (result != null)
            {
                ErrorMessage = result;
                return false;
            }
        
            return true;
        }
    
        private async Task<string> ValidateCreations()
        {
            if (!Creations.Any())
                return $"Array 'creations' cannot be null or empty";

            foreach (var creation in Creations)
            {
                var result = creation.Validate();
                if (result != null)
                    return await Task.FromResult(result);
            }
        
            return null;
        }

        private async Task<string> ValidateVariables()
        {
            foreach (var variable in Variables)
            {
                var result = variable.Validate();
                if (result != null)
                    return await Task.FromResult(result);
            }

            return null;
        }

        public async Task Create()
        {
            foreach (var creation in Creations)
            {
                if (creation.HasFile)
                {
                    var path = ReplaceVariables(creation.FilePath);
                    var content = ReplaceVariables(await creation.GetFileContent());
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