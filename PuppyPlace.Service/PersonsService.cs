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
}