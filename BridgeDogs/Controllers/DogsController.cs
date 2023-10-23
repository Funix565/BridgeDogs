using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeDogs.Data;
using BridgeDogs.Models;
using BridgeDogs.Interfaces;

namespace BridgeDogs.Controllers
{
    [Route("dogs")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogRepository _dogRepository;

        public DogsController(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        // GET: /dogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> GetDogs([FromQuery] DogParameters dogParameters)
        {
            if (!dogParameters.ValidAttributeAndOrder)
            {
                return BadRequest("Invalid attribute or order");
            }

            if (dogParameters.PageNumber < 0 || dogParameters.PageSize < 0)
            {
                return BadRequest("Negative pagination");
            }

            var dogs = await _dogRepository.GetAllDogsAsync(dogParameters);

            return Ok(dogs);
        }

        // POST: /dog
        [HttpPost]
        [Route("/dog")]
        public async Task<ActionResult<Dog>> PostDog([FromBody] Dog dog)
        {
            // TODO: How to test this case with Swagger or curl?
            // TODO: How to handle invalid JSON? Now it just ignores random field and assigns default values
            if (dog == null)
            {
                return BadRequest("Invalid JSON is passed in a request body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("My message");
            }

            if (string.IsNullOrEmpty(dog.Name))
            {
                return BadRequest();
            }

            // TODO: How to handle a case when we pass text where number expected? I want to return my own message
            if (dog.TailLength < 0)
            {
                return BadRequest("Tail length is a negative number.");
            }

            if (dog.Weight < 0)
            {
                return BadRequest("Weight is a negative number.");
            }

            if (await _dogRepository.DogExistsAsync(dog.Name))
            {
                return Conflict("Dog with the same name already exists in DB.");
            }

            try
            {
                var createdDog = await _dogRepository.CreateDogAsync(dog);

                // TODO: Hardcoded for now. Probably improve with CreatedAtAction.
                return Created($"/dogs/{createdDog.Name}", createdDog);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
