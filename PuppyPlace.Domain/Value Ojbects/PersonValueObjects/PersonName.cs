namespace PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

public class PersonName
{
    private readonly string _value;

    public PersonName(string value)
    {
        _value = value;
    }
    public string Value => _value;
    public override string ToString()
    {
        return _value;
    }

    public static implicit operator PersonName(string value) => new PersonName(value);
}