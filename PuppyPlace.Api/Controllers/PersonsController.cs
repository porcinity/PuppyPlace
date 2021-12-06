using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppyPlace.Domain;
using PuppyPlace.Data;
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
    public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
    {
        return await _personsService.FindPersons();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPerson(Guid id)
    {
        var person = await _personsService.FindPerson(id);

        if (person == null)
        {
            return NotFound();
        }

        return person;
    }

    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson(Person person)
    {
        await _personsService.AddPerson(person);
        return CreatedAtAction("GetPerson", new {id = person.Id}, person);
    }
    
    [HttpPost("{personId}/adoptdog")]
    public async Task AdoptDog(Guid personId, Guid dogId)
    {
        await _adoptionService.AdoptDog(personId, dogId);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePerson(Guid id)
    {
        await _personsService.DeletePerson(id);
        return NoContent();
    }
}