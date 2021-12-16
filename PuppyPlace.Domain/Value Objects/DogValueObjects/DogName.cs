using System.Text.RegularExpressions;

namespace PuppyPlace.Domain.Value_Objects.DogValueObjects;

public readonly record struct DogName
{
    public readonly string Value;

    private DogName(string value)
    {
        Value = value;
    }
    public static DogName Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new Exception();
        }
        
        if (value.Length > 30)
        {
            throw new Exception();
        }
        
        if (Regex.IsMatch(value, @"\s"))
        {
            throw new Exception();
        }

        if (Regex.IsMatch(value, @"[0-9]"))
        {
            throw new Exception();
        }
        
        if (value.Any(c => !char.IsLetter(c)))
        {
            throw new InvalidOperationException("Name cannot contain special chars.");
        }

        return new DogName(value);
    }
}