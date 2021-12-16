using System.Text.RegularExpressions;

namespace PuppyPlace.Domain.Value_Objects.PersonValueObjects;

public record PersonName
{
    private readonly string _value;

    public PersonName(string value)
    {
        if (IsValidName(value))
        {
            _value = value;
        }
    }
    public string Value => _value;
    public override string ToString()
    {
        return _value;
    }
    
    private bool IsValidName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyNameException();
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
        
        if (value.Any(c => !char.IsLetter(c)))
        {
            throw new InvalidOperationException("Name cannot contain special chars.");
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