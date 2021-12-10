using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuppyPlace.Api.DataTransferObjects;
using PuppyPlace.Domain;
using PuppyPlace.Repository;
using PuppyPlace.Service;
using PuppyPlace.Services.Persons.Commands;
using PuppyPlace.Services.Persons.Queries;
using PersonDto = PuppyPlace.Service.PersonDto;

namespace PuppyPlace.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{ 
    private readonly IPersonsService _personsService;
    private readonly IAdoptionService _adoptionService;
    private readonly IPersonsRepository _personsRepository;
    private readonly IMediator _mediator;

    public PersonsController(IPersonsService personsService, IAdoptionService adoptionService, IPersonsRepository personsRepository, IMediator mediator)
    {
        _personsService = personsService;
        _adoptionService = adoptionService;
        _personsRepository = personsRepository;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons(GetAllPersonsQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson([FromRoute] GetPersonByIdQuery query)
    {
        var response = await _mediator.Send(query);
        return response == null ? NotFound() : Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson([FromBody] CreatePersonCommand command)
    {
        var newPersonId = await _mediator.Send(command);
        return CreatedAtAction("GetPerson", new {id = newPersonId}, newPersonId);
    }
    
    [HttpPost("{personId}/adoptdog")]
    public async Task AdoptDog(Guid personId, AdoptDogDto adoptDogDto)
    {
        var dogId = adoptDogDto.Id;
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