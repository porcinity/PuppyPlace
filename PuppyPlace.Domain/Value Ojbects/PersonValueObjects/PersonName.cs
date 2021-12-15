namespace PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

public record PersonName
{
    private readonly string _value;

    public PersonName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyNameException();
        }
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new WhiteSpaceNameException();
        }
        if (value.Length > 30)
        {
            throw new NameTooLongException();
        }
        _value = value;
    }
    public string Value => _value;
    public override string ToString()
    {
        return _value;
    }
    public static implicit operator PersonName(string value) => new PersonName(value);
}

public class EmptyNameException : Exception
{
    public EmptyNameException() : base("Name cannot be empty.")
    {
    }
}

public class NameTooLongException : Exception
{
    public NameTooLongException() : base("Name is too long.")
    {
        
    }
}

public class WhiteSpaceNameException : Exception
{
    public WhiteSpaceNameException() : base("Name cannot contain whitespace.")
    {
        
    }
}