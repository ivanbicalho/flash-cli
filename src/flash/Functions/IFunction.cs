namespace flash.Functions
{
    public interface IFunction
    {
        string Name { get; }
        string Apply(string value);
        public string Search(string value) => $"{Name}({value})";
    }
}