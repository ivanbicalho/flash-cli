using System.IO;
using System.Threading.Tasks;

namespace flash
{
    public class Creation
    {
        public string Folder { get; set; }
        public string File { get; set; }

        public string Validate()
        {
            if (string.IsNullOrWhiteSpace(Folder) && string.IsNullOrWhiteSpace(File))
                return "Invalid 'creations', file or folder have to have a value";
        
            try
            {
                if (HasFolder)
                    Directory.Exists(Folder);

                if (HasFile)
                    System.IO.File.Exists(File);
            }
            catch
            {
                return $"Invalid 'creations', invalid format for file or folder";
            }
            
            if (!System.IO.File.Exists(FilePath))
                return $"Invalid 'creations', file '{File}' doesn't exist";
        
            return null;
        }

        public string FilePath => HasFile ? Path.Combine(FlashTemplates.FlashTemplatesFolder, File) : string.Empty;
        public bool HasFile => !string.IsNullOrWhiteSpace(File);
        public bool HasFolder => !string.IsNullOrWhiteSpace(Folder);

        public async Task<string> GetFileContent()
        {
            return await System.IO.File.ReadAllTextAsync(FilePath);
        }
    }
}