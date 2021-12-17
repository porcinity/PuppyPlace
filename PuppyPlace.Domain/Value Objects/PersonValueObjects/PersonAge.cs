namespace PuppyPlace.Domain.Value_Objects.PersonValueObjects;

public readonly record struct PersonAge
{
    public readonly int Value;
    private PersonAge(int value)
    {
        Value = value;
    }
    public static PersonAge Create(int value)
    {
        if (value < 1)
        {
            throw new Exception("Too young.");
        }
        if (value > 100)
        {
            throw new Exception("Too old.");
        }

        return new PersonAge(value);
    }
}