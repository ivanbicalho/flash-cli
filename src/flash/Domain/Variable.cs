using flash.Domain.Exceptions;
using flash.Model;

namespace flash.Domain
{
    public class Variable
    {
        public Variable(VariableModel variableModel)
        {
            Replace = variableModel.Replace;
            Question = variableModel.Question;
            
            if (string.IsNullOrWhiteSpace(Replace))
                throw new FlashException("Invalid 'variables', field replace cannot be null or empty", ErrorCodes.InvalidVariable);
        
            if (string.IsNullOrWhiteSpace(Question))
                throw new FlashException("Invalid 'variables', field question cannot be null or empty", ErrorCodes.InvalidVariable);
        }
        
        public string Replace { get; }
        public string Question { get; }
        public string Value { get; set; }

        public bool IsValueValid()
        {
            return !string.IsNullOrWhiteSpace(Value);
        }
    }
}