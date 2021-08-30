namespace flash.Functions
{
    public class Lower : IFunction
    {
        public string Name => "lower";
        
        public string Apply(string value)
        {
            return value?.ToLower();
        }
    }
}