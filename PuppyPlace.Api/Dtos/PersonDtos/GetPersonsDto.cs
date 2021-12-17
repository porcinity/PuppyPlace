using System.Security.Cryptography.X509Certificates;
using PuppyPlace.Domain;

namespace PuppyPlace.Api.Dtos;

public class GetPersonsDto
{
    private List<GetPersonDto> _people { get; } = new List<GetPersonDto>();
    public IEnumerable<GetPersonDto> peeps => _people;
    public static GetPersonsDto Create(IEnumerable<Person> persons)
    {
        var dto = new GetPersonsDto();
        
        foreach (var person in persons)
        {
            var personDto = GetPersonDto.FromPerson(person);
            dto._people.Add(personDto);
        }
        
        return dto;
    }
}