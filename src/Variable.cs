public class Variable
{
    public string Replace { get; set; }
    public string Question { get; set; }
    public string Value { get; set; }

    public string Validate()
    {
        if (string.IsNullOrWhiteSpace(Replace))
            return "Invalid 'variables', replace cannot be null or empty";
        
        if (string.IsNullOrWhiteSpace(Question))
            return "Invalid 'variables', question cannot be null or empty";
        
        if (string.IsNullOrWhiteSpace(Value))
            return "Invalid 'variables', value cannot be null or empty";

        return null;
    }
}