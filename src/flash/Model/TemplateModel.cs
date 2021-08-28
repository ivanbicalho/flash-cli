using System.Collections.Generic;
using flash.Domain;

namespace flash.Model
{
    public class TemplateModel
    {
        public IEnumerable<CreationModel> Creations { get; set; } = new List<CreationModel>();
        public IEnumerable<VariableModel> Variables { get; set; } = new List<VariableModel>();

        public Template ToDomainModel(string templateName, string flashTemplatesFolderPath)
        {
            return new Template(templateName, flashTemplatesFolderPath, this);
        }
    }
}