using BridgeDogs.Models;
using Microsoft.EntityFrameworkCore;

namespace BridgeDogs.Data
{
    public class DogshouseContext : DbContext
    {
        public DogshouseContext(DbContextOptions<DogshouseContext> options) : base(options)
        {
            
        }

        public DbSet<Dog> Dogs { get; set; }
    }
}
