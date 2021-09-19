using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using flash.Domain.Exceptions;
using flash.Model;

namespace flash.Domain
{
    public class FlashTemplates
    {
        private readonly string _flashTemplatesFolderPath;

        public FlashTemplates(string flashTemplatesFolderPath = null)
        {
            _flashTemplatesFolderPath = flashTemplatesFolderPath ?? Path.Combine(
                Path.GetDirectoryName(System.AppContext.BaseDirectory),
                "flash-templates");
        }

        private readonly List<Template> _templates = new();
        public IEnumerable<Template> Templates => _templates;
        public string ErrorMessage { get; private set; }
        public string ErrorCode { get; private set; }
        public bool IsValid => ErrorMessage == null;

        public async Task Load()
        {
            if (!Directory.Exists(_flashTemplatesFolderPath))
            {
                ErrorMessage = "Folder 'flash-templates' does not exist. Create one next to the executable";
                ErrorCode = ErrorCodes.MissingFlashTemplateFolder;
                return;
            }

            var folders = Directory.GetDirectories(_flashTemplatesFolderPath);
            if (!folders.Any())
            {
                ErrorMessage = "No templates were found in flash-templates folder";
                ErrorCode = ErrorCodes.MissingTemplates;
                return;
            }

            foreach (var folder in folders)
            {
                var templateName = new DirectoryInfo(folder).Name;
                Template template;

                if (EmptyDirectory(folder))
                {
                    ErrorMessage = $"Empty template: no files to create in template folder '{templateName}'";
                    ErrorCode = ErrorCodes.EmptyTemplate;
                    return;
                }

                var config = Path.Combine(folder, Consts.ConfigFile);

                if (!File.Exists(config))
                {
                    template = new Template(templateName, null, _flashTemplatesFolderPath, new TemplateModel());
                    _templates.Add(template);
                    continue;
                }

                try
                {
                    var json = await File.ReadAllTextAsync(config);
                    var model = JsonSerializer.Deserialize<TemplateModel>(json,
                        new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

                    template = model.ToDomainModel(templateName, _flashTemplatesFolderPath);
                }
                catch (FlashException flashEx)
                {
                    ErrorMessage = $"Invalid template '{templateName}': {flashEx.Message}";
                    ErrorCode = flashEx.ErrorCode;
                    return;
                }
                catch
                {
                    ErrorMessage =
                        $"File '{Consts.ConfigFile}' is mal-formed in template folder '{templateName}'";
                    ErrorCode = ErrorCodes.InvalidConfigFile;
                    return;
                }
                
                _templates.Add(template);
            }
        }

        private bool EmptyDirectory(string folder)
        {
            var items = Directory.EnumerateFileSystemEntries(folder);

            if (!items.Any())
                return true;

            if (items.Count() == 1 && new FileInfo(items.First()).Name == Consts.ConfigFile)
                return true;

            return false;
        }

        public Template Get(string templateName)
        {
            return Templates.FirstOrDefault(item => item.Name == templateName);
        }
    }
}