using Microsoft.EntityFrameworkCore;
using PuppyPlace.Data;
using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public class DogsService
{
    private readonly PuppyPlaceContext _context;
    private readonly PersonsService _personsService;
    public DogsService(PuppyPlaceContext context, PersonsService personsService)
    {
        _context = context;
        _personsService = personsService;
    }
    public async Task AddDog(Dog dog)
    {
        await _context.Dogs.AddAsync(dog);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Dog>> FindDogs()
    {
        return await _context.Dogs.ToListAsync();
    }

    public async Task<List<string>> ListDogNames()
    {
        return await _context.Dogs.Select(d => d.Name).ToListAsync();
    }

    public async Task<Dog?> FindDog(Guid id)
    {
        return await _context.Dogs.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Dog?> FindDogWithOwner(Guid id)
    {
        return await _context.Dogs
            .Include("Owner")
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task UpdateDog(Dog dog)
    {
        _context.Dogs.Update(dog);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDog(Dog dog)
    {
        _context.Dogs.Remove(dog);
        await _context.SaveChangesAsync();
    }

    public async Task AddOwner(Dog dog, Person person)
    {
        dog.AddOwner(person);
        await _context.SaveChangesAsync();
    }
}