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
            var personDto = new GetPersonDto();
            personDto.Id = person.Id;
            personDto.Name = person.Name;
            personDto.Age = person.Age.Value;
            // personDto.Dogs = person.Dogs;
            dto._people.Add(personDto);
        }

        return dto;
    }
}