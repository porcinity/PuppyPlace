namespace PuppyPlace.Domain.Value_Objects.DogValueObjects;

public record DogAge
{
    public int Value { get; }

    private DogAge(int value)
    {
        Value = value;
    }

    public static DogAge Create(int value)
    {
        if (value < 0)
        {
            throw new Exception("Too young.");
        }
        if (value > 20)
        {
            throw new Exception("Too old.");
        }

        return new DogAge(value);
    }
}