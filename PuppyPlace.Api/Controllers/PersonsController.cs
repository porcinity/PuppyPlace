using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuppyPlace.Api.Dtos;
using PuppyPlace.Domain;
using PuppyPlace.Services.Persons.Commands;
using PuppyPlace.Services.Persons.Queries;

namespace PuppyPlace.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetPersonsDto>> GetPersons([FromRoute] GetAllPersonsQuery query)
    {
        var people = await _mediator.Send(query);
        var response = GetPersonsDto.Create(people);
        return Ok(response.peeps);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetPersonDto>> GetPerson([FromRoute] GetPersonByIdQuery query)
    {
        var person = await _mediator.Send(query);
        var response = GetPersonDto.FromPerson(person);
        return response == null ? NotFound() : Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson([FromBody] CreatePersonCommand command)
    {
        var person = await _mediator.Send(command);
        var response = GetPersonDto.FromPerson(person);
        return Ok(response);
    }
    
    [HttpPost("{personId}/adoptdog")]
    public async Task<ActionResult> AdoptDog(Guid personId, AdoptDogCommand command)
    {
        command.PersonId = personId;
        return Ok(await _mediator.Send(command));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(Guid id, PutPersonCommand command)
    {
        command.Id = id;
        var person = await _mediator.Send(command);
        var response = GetPersonDto.FromPerson(person);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePerson([FromRoute] DeletePersonCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}