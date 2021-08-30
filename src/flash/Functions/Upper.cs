namespace flash.Functions
{
    public class Upper : IFunction
    {
        public string Name => "upper";
        
        public string Apply(string value)
        {
            return value?.ToUpper();
        }
    }
}