using System.IO;
using System.Threading.Tasks;
using flash.Domain.Exceptions;
using flash.Model;

namespace flash.Domain
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
                throw new FlashException("Invalid 'creations', file or folder have to have a value",
                    ErrorCodes.InvalidCreationFields);

            if (HasFolder && Folder.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                throw new FlashException($"Invalid 'creations', invalid format for folder",
                    ErrorCodes.InvalidFileOrFolderFormat);

            if (HasFile && File.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new FlashException($"Invalid 'creations', invalid format for file",
                    ErrorCodes.InvalidFileOrFolderFormat);

            if (!System.IO.File.Exists(FilePath))
                throw new FlashException($"Invalid 'creations', file '{File}' doesn't exist",
                    ErrorCodes.MissingCreationFile);
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