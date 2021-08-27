using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class Templates
{
    internal static readonly string FlashTemplatesFolder = "";
    
    private readonly List<Template> _items = new List<Template>();
    public IEnumerable<Template> Items => _items;
    public string ErrorMessage { get; private set; }
    public bool IsValid => ErrorMessage == null;

    public async Task Load()
    {
        if (!Directory.Exists(FlashTemplatesFolder))
        {
            ErrorMessage = "Folder 'flash-templates' does not exist. Create one besides the executable";
            return;
        }
        
        var folders = Directory.GetDirectories(FlashTemplatesFolder);
        foreach (var folder in folders)
        {
            try
            {
                var json = await File.ReadAllTextAsync(Path.Combine(folder, "config.json"));
                var template = JsonSerializer.Deserialize<Template>(json);
                
                if (!await template.Validate())
                {
                    _items.Clear();
                    ErrorMessage = $"Invalid template '{template.Name}': {template.ErrorMessage}";
                    return;
                }
                
                _items.Add(template);
            }
            catch
            {
                ErrorMessage = $"File 'config.json' doesn't exist or it's mal-formed in template folder '{folder}'";
            }
        }
    }

    public Template Get(string templateName)
    {
        return Items.FirstOrDefault(item => item.Name == templateName);
    }
}