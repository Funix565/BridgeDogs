using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeDogs.Data;
using BridgeDogs.Models;


// TODO: Rewrite using Repository
// TODO: Pay attention to what I return. Preferable not just plain status code, but `CreatedAtAction`

namespace BridgeDogs.Controllers
{
    [Route("dogs")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DogshouseContext _context;

        public DogsController(DogshouseContext context)
        {
            _context = context;
        }

        // GET: /dogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> GetDogs()
        {
          if (_context.Dogs == null)
          {
              return NotFound();
          }
            return await _context.Dogs.ToListAsync();
        }

        // POST: /dog
        [HttpPost]
        [Route("/dog")]
        public async Task<ActionResult<Dog>> PostDog(Dog dog)
        {
          if (_context.Dogs == null)
          {
              return Problem("Entity set 'DogshouseContext.Dogs'  is null.");
          }
            _context.Dogs.Add(dog);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DogExists(dog.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status201Created);
            // return CreatedAtAction("GetDog", new { id = dog.Name }, dog);
        }

        private bool DogExists(string id)
        {
            return (_context.Dogs?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
