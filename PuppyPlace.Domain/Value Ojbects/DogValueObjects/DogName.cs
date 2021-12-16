using System.Text.RegularExpressions;

namespace PuppyPlace.Domain.Value_Ojbects.DogValueObjects;

public readonly record struct DogName
{
    private readonly string _value;

    private DogName(string value)
    {
        _value = value;
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