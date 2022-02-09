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
        var response = people.Select(GetPersonDto.FromPerson);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetPersonDto>> GetPerson([FromRoute] GetPersonByIdQuery query)
    {
        var person = await _mediator.Send(query);
        var response = GetPersonDto.FromPerson(person);
        return response == null ? NotFound() : Ok(response);
    }

    [HttpGet("{id}/dogs")]
    public async Task<ActionResult<IEnumerable<GetDogDto>>> ShowDogs([FromRoute] GetPersonByIdQuery query)
    {
        var person = await _mediator.Send(query);
        var response = person.Dogs.Select(GetDogDto.FromDog);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostPerson([FromBody] CreatePersonCommand command)
    {
        var person = await _mediator.Send(command);
        return person.Match<IActionResult>(p => Ok(GetPersonDto.FromPerson(p)), () => NotFound());
    }
    
    [HttpPost("{personId}/adoptdog")]
    public async Task<ActionResult> AdoptDog(Guid personId, AdoptDogDto adoptDogDto)
    {
        var command = AdoptDogCommand.Create(personId, adoptDogDto.Id);
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