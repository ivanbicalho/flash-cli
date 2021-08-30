namespace flash.Functions
{
    public class Camel : IFunction
    {
        public string Name => "camel";
        
        public string Apply(string value)
        {
            return value != null ? string.Concat(char.ToLower(value[0]), value[1..]) : null;
        }
    }
}