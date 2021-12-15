namespace PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

public record PersonAge
{
    public int Value { get; }

    public PersonAge(int value)
    {
        Value = value;
    }
}