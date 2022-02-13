using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuppyPlace.Api.Dtos;
using PuppyPlace.Domain;
using PuppyPlace.Services.Dogs.Commands;
using PuppyPlace.Services.Dogs.Queries;


namespace PuppyPlace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetDogs([FromRoute] GetAllDogsQuery query)
        {
            return await _mediator
                .Send(query)
                .Select(x => x.Select(GetDogDto.FromDog))
                .Map(Ok);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDog([FromRoute] GetDogByIdQuery query)
        {
            var dog = await _mediator.Send(query);
            return dog
                .Map(GetDogDto.FromDog)
                .Some<IActionResult>(Ok)
                .None(NotFound);

        }

        [HttpPost]
        public async Task<IActionResult> PostDog([FromBody] CreateDogCommand command)
        {
            var dog = await _mediator.Send(command);
            return dog
                .Succ<IActionResult>(d => Ok(GetDogDto.FromDog))
                .Fail(e =>
                {
                    var errors = e.Select(x => x.Message).ToList();
                    return UnprocessableEntity(new {errors});
                });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetDogDto>> PutDog(Guid id, [FromBody] PutDogCommand command)
        {
            command.Id = id;
            var dog = await _mediator.Send(command);
            var response = GetDogDto.FromDog(dog);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDog([FromRoute] DeleteDogCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
