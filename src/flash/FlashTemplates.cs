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
        internal static readonly string FlashTemplatesFolder =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private readonly List<Template> _templates = new();
        public IEnumerable<Template> Templates => _templates;
        public string ErrorMessage { get; private set; }
        public bool IsValid => ErrorMessage == null;

        public async Task Load()
        {
            if (!Directory.Exists(FlashTemplatesFolder))
            {
                ErrorMessage = "Folder 'flash-templates' does not exist. Create one next to the executable";
                return;
            }

            var folders = Directory.GetDirectories(FlashTemplatesFolder);
            foreach (var folder in folders)
            {
                string json;
                Template template;

                try
                {
                    json = await File.ReadAllTextAsync(Path.Combine(folder, "config.json"));
                    template = JsonSerializer.Deserialize<Template>(json);
                }
                catch
                {
                    ErrorMessage = $"File 'config.json' doesn't exist or it's mal-formed in template folder '{folder}'";
                    return;
                }

                if (!await template.Validate())
                {
                    _templates.Clear();
                    ErrorMessage = $"Invalid template '{template.Name}': {template.ErrorMessage}";
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