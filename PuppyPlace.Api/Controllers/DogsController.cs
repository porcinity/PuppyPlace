using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuppyPlace.Api.Dtos;
using PuppyPlace.Domain;
using PuppyPlace.Service;
using PuppyPlace.Services.Dogs.Commands;
using PuppyPlace.Services.Dogs.Queries;


namespace PuppyPlace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DogsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<GetDogsDto>> GetDogs([FromRoute] GetAllDogsQuery query)
        {
            var dogs = await _mediator.Send(query);
            var response = GetDogsDto.Create(dogs);
            return Ok(response.Dogs);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDogDto>> GetDog([FromRoute] GetDogByIdQuery query)
        {
            var dog = await _mediator.Send(query);
            var response = GetDogDto.Create(dog);
            return response == null ? NotFound() : Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<PostDogDto>> PostDog([FromBody] CreateDogCommand command)
        {
            var dog =  await _mediator.Send(command);
            var response = GetDogDto.Create(dog);
            return Ok(response);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GetDogDto>> PutDog(Guid id, [FromBody] PutDogCommand command)
        {
            command.Id = id;
            var dog = await _mediator.Send(command);
            var response = GetDogDto.Create(dog);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDog([FromRoute] DeleteDogCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
