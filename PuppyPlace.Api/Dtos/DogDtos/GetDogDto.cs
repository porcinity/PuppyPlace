using PuppyPlace.Domain;

namespace PuppyPlace.Api.Dtos;

public class GetDogDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Age { get; init; }
    public string Breed { get; init; }

    public static GetDogDto Create(Dog dog)
    {
        return new GetDogDto
        {
            Id = dog.Id,
            Name = dog.Name,
            Age = dog.Age,
            Breed = dog.Breed
        };
    }
}