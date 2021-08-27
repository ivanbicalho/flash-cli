using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace flash
{
    public class TemplateModel
    {
        public IEnumerable<CreationModel> Creations { get; set; }
        public IEnumerable<VariableModel> Variables { get; set; }

        public Template ToDomainModel(string templateName, string flashTemplatesFolderPath)
        {
            return new Template(templateName, flashTemplatesFolderPath, this);
        }
    }
}