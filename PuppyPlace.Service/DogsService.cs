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

    public async void AddDogDb(Dog dog)
    {
        await _context.Dogs.AddAsync(dog);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Dog>> FindDogs()
    {
        return await _context.Dogs.ToListAsync();
    }

    public async Task<Dog> FindDog(Guid id)
    {
        try
        {
            return await _context.Dogs.FindAsync(id);
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async void UpdateDog(Guid id, string name, int age, string breed)
    {
        var dog = await FindDog(id);
        dog.Name = name;
        dog.Age = age;
        dog.Breed = breed;
        await _context.SaveChangesAsync();
    }

    public async void DeleteDogDb(Dog dog)
    {
        _context.Dogs.Remove(dog);
        await _context.SaveChangesAsync();
    }

    public async void AddOwnerDb(Dog dog, Person person)
    {
        dog.Owner = person;
        _personsService.AdoptDog(person, dog);
        await _context.SaveChangesAsync();
    }
}