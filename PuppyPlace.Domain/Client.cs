using PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

namespace PuppyPlace.Domain;

public class Client
{
    public Guid Id { get; set; }

    public PersonName Name { get; set; }
}