using Microsoft.EntityFrameworkCore;
using PuppyPlace.Domain;
using PuppyPlace.Repository;

namespace PuppyPlace.Service;

public class PersonsService : IPersonsService
{
    private readonly IPersonsRepository _personsRepository;

    public PersonsService(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }
    private static PersonDTO ItemToDTO(Person person) =>
        new PersonDTO
        {
            Id = person.Id, 
            Name = person.Name
        };
    public async Task<List<PersonDTO>> FindPersons()
    {
        return await _personsRepository
            .FindPersons()
            .Select(m => ItemToDTO(m))
            .ToListAsync();
    }

    public async Task<PersonDTO> FindPerson(Guid id)
    {
        var person = await _personsRepository.FindPerson(id);
        return ItemToDTO(person);
    }

    public async Task AddPerson(PersonDTO personDto)
    {
        Person person = new(personDto.Name);
        await _personsRepository.AddPerson(person);
    }


    // public Task DeletePerson()
    // {
    //     throw new NotImplementedException();
    // }
}