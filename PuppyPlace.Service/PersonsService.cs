using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PuppyPlace.Data;
using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public class PersonsService
{
    private readonly PuppyPlaceContext _context = new PuppyPlaceContext();

    public async void AddPersonDb(Person person)
    {
       await _context.Persons.AddAsync(person);
       await _context.SaveChangesAsync();
    }

    public async void AdoptDog(Person person, Dog dog)
    {
        person.AddDog(dog);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Person>> FindPersons()
    {
        return await _context.Persons.ToListAsync();
    }

    public async Task<Person> FindPerson(Guid id)
    {
        try
        {
            return await _context.Persons.FindAsync(id);
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async void UpdatePerson(Guid id, string name)
    {
        var person = await FindPerson(id);
        person.Name = name;
        await _context.SaveChangesAsync();
    }

    public async void DeletePersonDb(Person person)
    {
        // var person = await FindPerson(id);
        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
    }
}