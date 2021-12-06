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

    public PersonsController(IPersonsService personsService)
    {
        _personsService = personsService;
    }

    // GET: api/Dogs
    [HttpGet]
    public async Task<List<Person>> GetPersons()
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
}