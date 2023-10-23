using BridgeDogs.Data;
using BridgeDogs.Interfaces;
using BridgeDogs.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BridgeDogs.Repository
{
    public class DogRepository : IDogRepository
    {
        private readonly DogshouseContext _context;

        public DogRepository(DogshouseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dog>> GetAllDogsAsync(DogParameters dogParameters)
        {
            var query = _context.Dogs.AsQueryable();
            if (dogParameters.OrderBy == "asc")
            {
                query = query.OrderBy($"{dogParameters.Attribute} asc");
            }
            else
            {
                query = query.OrderBy($"{dogParameters.Attribute} desc");
            }

            return await query
                .Skip((dogParameters.PageNumber - 1) * dogParameters.PageSize)
                .Take(dogParameters.PageSize)
                .ToListAsync();
        }

        public async Task<Dog> CreateDogAsync(Dog dog)
        {
            _context.Dogs.Add(dog);
            await _context.SaveChangesAsync();
            return dog;
        }

        public async Task<bool> DogExistsAsync(string name)
        {
            return await _context.Dogs?.AnyAsync(d => d.Name == name);
        }

        
    }
}
