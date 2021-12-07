using Microsoft.AspNetCore.Mvc;
using PuppyPlace.Domain;
using PuppyPlace.Service;

namespace PuppyPlace.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{ 
    private readonly IPersonsService _personsService;
    private readonly IAdoptionService _adoptionService;
    public PersonsController(IPersonsService personsService, IAdoptionService adoptionService)
    {
        _personsService = personsService;
        _adoptionService = adoptionService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPersons()
    {
        return await _personsService.FindPersons();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDTO>> GetPerson(Guid id)
    {
        var person = await _personsService.FindPerson(id);

        if (person == null)
        {
            return NotFound();
        }

        return person;
    }

    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson(PersonDTO personDto)
    {
        await _personsService.AddPerson(personDto);
        return CreatedAtAction("GetPerson", new {id = personDto.Id}, personDto);
    }
    
    [HttpPost("{personId}/adoptdog")]
    public async Task AdoptDog(Guid personId, Guid dogId)
    {
        await _adoptionService.AdoptDog(personId, dogId);
    }
    
    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutPerson(Guid id, Person person)
    // {
    //     if (id != person.Id)
    //     {
    //         return BadRequest();
    //     }
    //
    //     // _context.Entry(todoItem).State = EntityState.Modified;
    //     //
    //     // try
    //     // {
    //     //     await _context.SaveChangesAsync();
    //     // }
    //     // catch (DbUpdateConcurrencyException)
    //     // {
    //     //     if (!TodoItemExists(id))
    //     //     {
    //     //         return NotFound();
    //     //     }
    //     //     else
    //     //     {
    //     //         throw;
    //     //     }
    //     // }
    //
    //     await _personsService.UpdatePerson(person);
    //     return NoContent();
    // }
    
    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeletePerson(Guid id)
    // {
    //     await _personsService.DeletePerson(id);
    //     return NoContent();
    // }
}