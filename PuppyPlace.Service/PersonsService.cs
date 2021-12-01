using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PuppyPlace.Data;
using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public class PersonsService
{
    private readonly PuppyPlaceContext _context;

    public PersonsService(PuppyPlaceContext context)
    {
        _context = context;
    }
    public async void AddPersonDb(Person person)
    {
       await _context.Persons.AddAsync(person);
       await _context.SaveChangesAsync();
    }

    public async Task AdoptDog(Person person, Dog dog)
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
            return await _context
                .Persons
                .Include(m => m.Dogs)
                .FirstAsync(m => m.Id == id);
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task UpdatePerson(Person person)
    {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePersonDb(Person person)
    {
        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
    }
}