using Microsoft.AspNetCore.Mvc;
using PuppyPlace.Domain;
using PuppyPlace.Service;


namespace PuppyPlace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogsService _dogsService;
        private readonly IMediator _mediator;

        public DogsController(IDogsService dogsService, IMediator mediator)
        {
            _dogsService = dogsService;
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DogDto>>> GetDogs()
        {
            return Ok(await _dogsService.FindDogs());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<DogDto>> GetDog(Guid id)
        {
            return await _dogsService.FindDog(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<Dog>> PostDog(DogDto dogDto)
        {
            await _dogsService.AddDog(dogDto);
            return CreatedAtAction("GetDog", new {id = dogDto.Id}, dogDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDog(Guid id, DogDto dogDto)
        {
            if (id != dogDto.Id)
            {
                return BadRequest();
            }
        
            await _dogsService.UpdateDog(dogDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDog(Guid id)
        {
            await _dogsService.DeleteDog(id);
            return NoContent();
        }
    }
}
