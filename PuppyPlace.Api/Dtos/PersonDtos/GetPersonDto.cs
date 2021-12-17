using PuppyPlace.Domain;

namespace PuppyPlace.Api.Dtos;

public class GetPersonDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}