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
            Location = creationModel.Location;
            TemplateFile = creationModel.TemplateFile;

            if (string.IsNullOrWhiteSpace(Location) && string.IsNullOrWhiteSpace(TemplateFile))
                throw new FlashException("Invalid 'creations', location or template file must have a value",
                    ErrorCodes.InvalidCreationFields);

            if (HasLocation && Location.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                throw new FlashException($"Invalid 'creations', invalid format for location",
                    ErrorCodes.InvalidLocationOrTemplateFile);

            if (HasTemplateFile && TemplateFile.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new FlashException($"Invalid 'creations', invalid format for template file",
                    ErrorCodes.InvalidLocationOrTemplateFile);

            if (HasTemplateFile && !File.Exists(TemplateFilePath))
                throw new FlashException($"Invalid 'creations', file '{TemplateFile}' doesn't exist",
                    ErrorCodes.MissingTemplateFile);
        }

        public string Location { get; }
        public string TemplateFile { get; }

        public string TemplateFilePath => HasTemplateFile ? Path.Combine(_templateFolderPath, TemplateFile) : string.Empty;
        public bool HasTemplateFile => !string.IsNullOrWhiteSpace(TemplateFile);
        public bool HasLocation => !string.IsNullOrWhiteSpace(Location);
        public string WritingPath => Path.Combine(Location ?? string.Empty, TemplateFile ?? string.Empty);

        public async Task<string> GetFileContent()
        {
            return await File.ReadAllTextAsync(TemplateFilePath);
        }
    }
}