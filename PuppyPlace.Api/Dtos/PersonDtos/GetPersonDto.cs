using PuppyPlace.Domain;

namespace PuppyPlace.Api.Dtos;

public class GetPersonDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Age { get; init; }

    public static GetPersonDto Create(Person person)
    {
        return new GetPersonDto
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age.Value
        };
    }
}