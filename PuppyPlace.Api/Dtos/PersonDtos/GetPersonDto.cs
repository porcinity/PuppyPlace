using PuppyPlace.Domain;

namespace PuppyPlace.Api.Dtos;

public class GetPersonDto
{
    public string Name { get; set; }
    public IEnumerable<PersonDogsDto> Dogs { get; set; }
}