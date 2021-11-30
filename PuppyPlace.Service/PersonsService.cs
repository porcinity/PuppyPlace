using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PuppyPlace.Data;
using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public class PersonsService
{
    private readonly PuppyPlaceContext _context = new PuppyPlaceContext();
    
    public void AddPersonDb(Person person)
    {
        _context.Persons.Add(person);
        _context.SaveChanges();
    }
    
    public void AdoptDog(Person person, Dog dog)
    {
        person.AddDog(dog);
        _context.Update(person);
        _context.SaveChanges();
    }
    public void ShowPersons()
    {
        var persons = _context.Persons.ToList();
        Console.WriteLine($"Person count is {persons.Count}");
        Console.WriteLine("We have the following people in our database");
        foreach (var person in persons)
        {
            Console.WriteLine(person.Name);
            foreach (var dog in person.Dogs)
            {
                Console.WriteLine($"- {dog.Name}");
            }
        }
    }
    public Person FindPerson(Guid id)
    {
        try
        {
            return _context.Persons.Find(id);
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}