using BridgeDogs.Models;
using Microsoft.EntityFrameworkCore;

namespace BridgeDogs.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DogshouseContext context)
        {
            if (context.Dogs.Any())
            {
                return;
            }

            var dogs = new Dog[]
            {
                new Dog { Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32 },
                new Dog { Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14 }
            };

            context.Dogs.AddRange(dogs);
            context.SaveChanges();
        }
    }
}
