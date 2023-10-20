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

        // TODO: Perhaps this is very naive approach. But it is ME who created it; by myself.
        // Better Guide: https://code-maze.com/paging-aspnet-core-webapi/
        public async Task<IEnumerable<Dog>> GetAllDogsAsync(string attribute = "name", string order = "asc", int pageNumber = 1, int pageSize = 10)
        {
            switch (attribute.ToLower())
            {
                case "name":
                    return (order == "asc")
                        ? await _context.Dogs.OrderBy(d => d.Name)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync()
                        : await _context.Dogs.OrderByDescending(d => d.Name)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
                case "color":
                    return (order == "asc")
                        ? await _context.Dogs.OrderBy(d => d.Color)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync()
                        : await _context.Dogs.OrderByDescending(d => d.Color)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
                case "tail_length":
                    return (order == "asc")
                        ? await _context.Dogs.OrderBy(d => d.TailLength)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync()
                        : await _context.Dogs.OrderByDescending(d => d.TailLength)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
                default:
                    return (order == "asc")
                        ? await _context.Dogs.OrderBy(d => d.Weight)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync()
                        : await _context.Dogs.OrderByDescending(d => d.Weight)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
            }
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
