using BridgeDogs.Data;
using BridgeDogs.Interfaces;
using BridgeDogs.Models;
using Microsoft.EntityFrameworkCore;

namespace BridgeDogs.Repository
{
    public class DogRepository : IDogRepository
    {
        private readonly DogshouseContext _context;

        public DogRepository(DogshouseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dog>> GetAllDogsAsync()
        {
            return await _context.Dogs.ToListAsync();
        }

        public async Task<Dog> CreateDogAsync(Dog dog)
        {
            _context.Dogs.Add(dog);

            try
            {
                await _context.SaveChangesAsync();
                return dog;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> DogExistsAsync(string name)
        {
            return await _context.Dogs?.AnyAsync(d => d.Name == name);
        }

        
    }
}
