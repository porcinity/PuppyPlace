using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<DogDto>>> GetDogs([FromRoute] GetAllDogsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDogDto>> GetDog([FromRoute] GetDogByIdQuery query)
        {
            var response = await _mediator.Send(query);
            return response == null ? NotFound() : Ok(_mapper.Map<GetDogDto>(response));
        }
        
        [HttpPost]
        public async Task<ActionResult<Dog>> PostDog([FromBody] CreateDogCommand command)
        {
            var dogId = await _mediator.Send(command);
            return CreatedAtAction("GetDog", new {id = dogId}, dogId);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDog(Guid id, [FromBody] PutDogCommand command)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDog([FromRoute] DeleteDogCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
