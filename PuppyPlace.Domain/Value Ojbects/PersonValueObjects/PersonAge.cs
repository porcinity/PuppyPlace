namespace PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

public record PersonAge
{
    public int Value { get; }

    public PersonAge(int value)
    {
        if (IsValidAge(value))
        {
            Value = value;
        }
    }

    private bool IsValidAge(int value)
    {
        if (value < 1)
        {
            throw new Exception("Too young.");
        }
        if (value > 100)
        {
            throw new Exception("Too old.");
        }

        return true;
    }
}