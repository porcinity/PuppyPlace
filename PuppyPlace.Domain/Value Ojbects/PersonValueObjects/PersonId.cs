namespace PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

public record PersonId
{
    public Guid Value { get; }

    public PersonId(Guid value)
    {
        Value = value;
    }
}