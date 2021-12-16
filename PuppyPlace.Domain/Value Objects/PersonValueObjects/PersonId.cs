namespace PuppyPlace.Domain.Value_Objects.PersonValueObjects;

public record PersonId
{
    public Guid Value { get; }

    public PersonId(Guid value)
    {
        Value = value;
    }
}