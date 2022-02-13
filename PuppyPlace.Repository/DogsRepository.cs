using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.SomeHelp;
using Microsoft.EntityFrameworkCore;
using PuppyPlace.Data;
using PuppyPlace.Domain;

namespace PuppyPlace.Repository;

public class DogsRepository : IDogsRepository
{
    private readonly PuppyPlaceContext _context;
    public DogsRepository(PuppyPlaceContext context)
    {
        _context = context;
    }
    public async Task<Unit> AddDog(Dog dog)
    {
        await _context.Dogs.AddAsync(dog);
        await _context.SaveChangesAsync();
        return unit;
    }
    public async Task<IList<Dog>> FindDogs()
    {
        return await _context.Dogs.AsNoTracking().ToListAsync();
    }
    
    public async Task<List<string>> ListDogNames()
    {
        return await _context.Dogs.Select(d => d.Name).ToListAsync();
    }

    public async Task<Option<Dog>> FindDog(Guid id)
    {
        var result = await _context.Dogs.FirstOrDefaultAsync(m => m.Id == id);
        if (result is null)
            return Option<Dog>.None;

        return result.ToSome();
    }

    public async Task<Option<Dog>> FindDogWithOwner(Guid id)
    {
         var result = await _context.Dogs
            .Include("Owner")
            .FirstOrDefaultAsync(m => m.Id == id);
         if (result is null)
             return Option<Dog>.None;
         return result.ToSome();
    }

    public async Task<Unit> UpdateDog(Dog dog)
    {
        _context.Dogs.Update(dog);
        await _context.SaveChangesAsync();
        return unit;
    }
    public async Task<Unit> UpdateDog (Guid id)
    {
        var dog = await FindDog(id);
        ignore(dog.Map(async d =>
        {
            _context.Dogs.Update(d);
            await _context.SaveChangesAsync();
        }));
        return unit;
    }
    public async Task DeleteDog(Dog dog)
    {
        _context.Dogs.Remove(dog);
        await _context.SaveChangesAsync();
    }
    public async Task<Option<Unit>> DeleteDog(Guid id)
    {
        var dog = await FindDog(id);
        ignore(dog.Map(async d =>
        {
            _context.Dogs.Remove(d);
            await _context.SaveChangesAsync();
        }));
        return dog.Map(d => unit);
    }
    public async Task AddOwner(Dog dog, Person person)
    {
        dog.AddOwner(person);
        await _context.SaveChangesAsync();
    }
}