using System.IO;
using System.Threading.Tasks;

namespace flash
{
    public class Creation
    {
        private readonly string _templateFolderPath;
        
        public Creation(CreationModel creationModel, string templateFolderPath)
        {
            _templateFolderPath = templateFolderPath;
            Folder = creationModel.Folder;
            File = creationModel.File;
            
            if (string.IsNullOrWhiteSpace(Folder) && string.IsNullOrWhiteSpace(File))
                throw new FlashException("Invalid 'creations', file or folder have to have a value", "invalid_creation_fields");

            try
            {
                if (HasFolder)
                    Directory.Exists(Folder);

                if (HasFile)
                    System.IO.File.Exists(File);
            }
            catch
            {
                throw new FlashException($"Invalid 'creations', invalid format for file or folder", "invalid_creation_format");
            }

            if (!System.IO.File.Exists(FilePath))
                throw new FlashException($"Invalid 'creations', file '{File}' doesn't exist", "invalid_creation_missed_file");
        }

        public string Folder { get; }
        public string File { get; }

        public string FilePath => HasFile ? Path.Combine(_templateFolderPath, File) : string.Empty;
        public bool HasFile => !string.IsNullOrWhiteSpace(File);
        public bool HasFolder => !string.IsNullOrWhiteSpace(Folder);
        public string WritingPath => Path.Combine(Folder ?? string.Empty, File ?? string.Empty);
        
        public async Task<string> GetFileContent()
        {
            return await System.IO.File.ReadAllTextAsync(FilePath);
        }
    }
}