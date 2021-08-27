using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace flash
{
    public class FlashTemplates
    {
        private readonly string _flashTemplatesFolderPath;

        public FlashTemplates(string flashTemplatesFolderPath)
        {
            _flashTemplatesFolderPath = flashTemplatesFolderPath;
        }

        private readonly List<Template> _templates = new();
        public IEnumerable<Template> Templates => _templates;
        public string ErrorMessage { get; private set; }
        public bool IsValid => ErrorMessage == null;

        public async Task Load()
        {
            if (!Directory.Exists(_flashTemplatesFolderPath))
            {
                ErrorMessage = "Folder 'flash-templates' does not exist. Create one next to the executable";
                return;
            }

            var folders = Directory.GetDirectories(_flashTemplatesFolderPath);
            foreach (var folder in folders)
            {
                Template template;

                try
                {
                    var json = await File.ReadAllTextAsync(Path.Combine(folder, "config.json"));
                    var model = JsonSerializer.Deserialize<TemplateModel>(json,
                        new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

                    var templateName = new DirectoryInfo(folder).Name;
                    template = model.ToDomainModel(templateName, _flashTemplatesFolderPath);
                }
                catch (FlashException flashEx)
                {
                    ErrorMessage = $"Invalid template '{folder}': {flashEx.Message}";
                    return;
                }
                catch
                {
                    ErrorMessage = $"File 'config.json' doesn't exist or it's mal-formed in template folder '{folder}'";
                    return;
                }

                _templates.Add(template);
            }
        }

        public Template Get(string templateName)
        {
            return Templates.FirstOrDefault(item => item.Name == templateName);
        }
    }
}