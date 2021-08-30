namespace flash.Functions
{
    public class Pascal : IFunction
    {
        public string Name => "pascal";
        
        public string Apply(string value)
        {
            return value != null ? string.Concat(char.ToUpper(value[0]), value[1..]) : null;
        }
    }
}