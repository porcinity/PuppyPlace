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

        public DogsController(IDogsService dogsService)
        {
            _dogsService = dogsService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> GetDogs()
        {
            return await _dogsService.FindDogs();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Dog>> GetDog(Guid id)
        {
            return await _dogsService.FindDog(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<Dog>> PostDog(Dog dog)
        {
            await _dogsService.AddDog(dog);
            return CreatedAtAction("GetDog", new {id = dog.Id}, dog);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDog(Guid id, Dog dog)
        {
            if (id != dog.Id)
            {
                return BadRequest();
            }

            await _dogsService.UpdateDog(dog);
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
