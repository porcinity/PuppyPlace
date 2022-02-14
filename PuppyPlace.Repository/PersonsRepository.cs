using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.SomeHelp;
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
    
    public async Task<Option<Person>> FindPerson(Guid id)
    {
        var person = await _context.Persons
                .AsNoTracking()
                .Include(m => m.Dogs)
                .FirstOrDefaultAsync(m => m.Id == id);

        if (person is null)
            return Option<Person>.None;

        return person.ToSome();
    }
    
    public async Task<Unit> AddPerson(Person person)
    {
       await _context.Persons.AddAsync(person);
       await _context.SaveChangesAsync();
       return unit;
    }

    public async Task AdoptDog(Person person, Dog dog)
    {
        person.AddDog(dog);
        await _context.SaveChangesAsync();
    }

    public async Task<Unit> UpdatePerson(Person person)
    {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync();
        return unit;
    }

    public async Task<Option<Unit>> DeletePerson(Guid id)
    {
        var person = await FindPerson(id);
        ignore(person.Map(async p=>
        {
            _context.Persons.Remove(p);
            await _context.SaveChangesAsync();
        }));
        return person.Map(p => unit);
    }
    
    public async Task<Unit> DeletePerson(Person person)
    {
        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
        return unit;
    }
}