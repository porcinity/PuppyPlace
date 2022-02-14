using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuppyPlace.Api.Dtos;
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
    public async Task<IActionResult> GetPersons([FromRoute] GetAllPersonsQuery query)
    {
        return await _mediator.Send(query)
            .Select(x => x.Select(GetPersonDto.FromPerson))
            .Select(Ok);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson([FromRoute] GetPersonByIdQuery query)
    {
        var person = await _mediator.Send(query);
        return person
            .Map(GetPersonDto.FromPerson)
            .Match<IActionResult>(Ok, NotFound);
    }

    [HttpGet("{id}/dogs")]
    public async Task<IActionResult> ShowDogs([FromRoute] GetPersonByIdQuery query)
    {
        var person = await _mediator.Send(query);
        return person
            .Map(x => x.Dogs.Select(GetDogDto.FromDog))
            .Match<IActionResult>(Ok, NotFound);
    }

    [HttpPost]
    public async Task<IActionResult> PostPerson([FromBody] CreatePersonCommand command)
    {
        var person = await _mediator.Send(command);
        return person.Match<IActionResult>(
            p => Ok(GetPersonDto.FromPerson(p)),
            e =>
            {
                var errorList = e.Select(e => e.Message).ToList();
                return UnprocessableEntity(new {code = 422, errors = errorList});
            });
    }

    [HttpPost("{personId}/adoptdog")]
    public async Task<IActionResult> AdoptDog(Guid personId, AdoptDogDto adoptDogDto)
    {
        var command = AdoptDogCommand.Create(personId, adoptDogDto.Id);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(_ => Ok(), NotFound);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(Guid id, PutPersonCommand command)
    {
        command.Id = id;
        var person = await _mediator.Send(command);
        return person
            .Some(
            p =>
                p.Succ<IActionResult>(x =>
                        Ok(GetPersonDto.FromPerson(x)))
                    .Fail(e =>
                    {
                        var list = e.Select(x => x.Message).ToList();
                        return UnprocessableEntity(new {errors = list});
                    }))
            .None(NotFound);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson([FromRoute] DeletePersonCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(_ => NoContent(), NotFound);
    }
}