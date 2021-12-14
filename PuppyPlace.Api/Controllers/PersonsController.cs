using AutoMapper;
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
    private readonly IMapper _mapper;

    public PersonsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPersonDto>>> GetPersons([FromRoute] GetAllPersonsQuery query)
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
    public async Task<ActionResult> AdoptDog(Guid personId, AdoptDogCommand command)
    {
        command.PersonId = personId;
        return Ok(await _mediator.Send(command));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(Guid id, PutPersonCommand command)
    {
        command.Id = id;
        return Ok(await _mediator.Send(command));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePerson([FromRoute] DeletePersonCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}