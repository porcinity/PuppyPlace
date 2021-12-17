using Microsoft.EntityFrameworkCore;
using PuppyPlace.Data;
using PuppyPlace.Domain;

namespace PuppyPlace.Repository;

public class PersonsRepository : IPersonsRepository
{
    private readonly PuppyPlaceContext _context;
    public PersonsRepository(PuppyPlaceContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Person>> FindPersons()
    {
        return await _context.Set<Person>().AsNoTracking().ToListAsync();
    }
    
    public async Task<Person> FindPerson(Guid id)
    {
        var person = await _context.Persons
                .Include(m => m.Dogs)
                .FirstOrDefaultAsync(m => m.Id == id);
        if (person is null)
        {
            return null;
        }

        return person;
    }
    
    public async Task AddPerson(Person person)
    {
       await _context.Persons.AddAsync(person);
       await _context.SaveChangesAsync();
    }

    public async Task AdoptDog(Person person, Dog dog)
    {
        person.AddDog(dog);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePerson(Person person)
    {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePerson(Guid id)
    {
        var person = await FindPerson(id);
        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeletePerson(Person person)
    {
        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
    }
}