using System.Text.RegularExpressions;

namespace PuppyPlace.Domain.Value_Objects.DogValueObjects;

public record DogBreed
{
    public readonly string Value;

    private DogBreed(string value)
    {
        Value = value;
    }

    public static DogBreed Create(string value)
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

        return new DogBreed(value);
    }
}