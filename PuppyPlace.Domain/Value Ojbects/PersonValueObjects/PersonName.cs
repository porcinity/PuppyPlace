using System.Text.RegularExpressions;

namespace PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

public record PersonName
{
    private readonly string _value;

    public PersonName(string value)
    {
        if (IsValid(value))
        {
            _value = value;
        }
    }
    public string Value => _value;
    public override string ToString()
    {
        return _value;
    }

    private bool IsValid(string value)
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
        
        if (Regex.IsMatch(value, @"\s"))
        {
            throw new WhiteSpaceNameException();
        }

        if (Regex.IsMatch(value, @"[0-9]"))
        {
            throw new Exception();
        }

        return true;
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