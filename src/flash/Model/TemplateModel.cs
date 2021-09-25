using System.Collections.Generic;
using flash.Domain;

namespace flash.Model
{
    public class TemplateModel
    {
        public string Description { get; set; }
        public IEnumerable<VariableModel> Variables { get; set; } = new List<VariableModel>();

        public Template ToDomainModel(string templateName, string flashTemplatesFolderPath)
        {
            return new Template(templateName, Description, flashTemplatesFolderPath, this);
        }
    }
}