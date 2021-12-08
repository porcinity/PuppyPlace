using Microsoft.AspNetCore.Mvc;
using PuppyPlace.Domain;
using PuppyPlace.Repository;
using PuppyPlace.Service;

namespace PuppyPlace.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{ 
    private readonly IPersonsService _personsService;
    private readonly IAdoptionService _adoptionService;
    private readonly IPersonsRepository _personsRepository;

    public PersonsController(IPersonsService personsService, IAdoptionService adoptionService, IPersonsRepository personsRepository)
    {
        _personsService = personsService;
        _adoptionService = adoptionService;
        _personsRepository = personsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
    {
        return Ok(await _personsRepository.FindPersons());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetPerson(Guid id)
    {
        var person = await _personsService.FindPerson(id);
        if (person == null)
        {
            return NotFound();
        }

        return person;
    }

    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson(PersonDto personDto)
    {
        await _personsService.AddPerson(personDto);
        return CreatedAtAction("GetPerson", new {id = personDto.Id}, personDto);
    }
    
    [HttpPost("{personId}/adoptdog")]
    public async Task AdoptDog(Guid personId, Guid dogId)
    {
        await _adoptionService.AdoptDog(personId, dogId);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(Guid id, PersonDto personDto)
    {
        if (id != personDto.Id)
        {
            return BadRequest();
        }
        
        await _personsService.UpdatePerson(personDto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePerson(Guid id)
    {
        await _personsService.DeletePerson(id);
        return NoContent();
    }
}