using System.Runtime.CompilerServices;
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
            Name = person.Name,
            Dogs = person.Dogs
        };
    public async Task<IEnumerable<PersonDTO>> FindPersons()
    {
        var persons = await _personsRepository.FindPersons();
        return persons.Select(m => ItemToDTO(m)).ToList();
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

    public async Task UpdatePerson(PersonDTO personDto)
    {
        var person = await _personsRepository.FindPerson(personDto.Id);
        person.Name = personDto.Name;
        person.Id = personDto.Id;
        await _personsRepository.UpdatePerson(person);
    }

    public async Task DeletePerson(Guid id)
    {
        await _personsRepository.DeletePerson(id);
    }
}