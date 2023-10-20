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
                new Dog { Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14 },
                new Dog { Name = "Lisa", Color = "amber", TailLength = 12, Weight = 13 },
                new Dog { Name = "Homer", Color = "red", TailLength = 42, Weight = 34 },
                new Dog { Name = "Champion", Color = "brown", TailLength = 52, Weight = 35 },
                new Dog { Name = "Rick", Color = "black", TailLength = 282, Weight = 36 },
                new Dog { Name = "Morty", Color = "black", TailLength = 272, Weight = 37 },
                new Dog { Name = "Patrick", Color = "red & amber", TailLength = 262, Weight = 38 },
                new Dog { Name = "Bob", Color = "red & amber", TailLength = 252, Weight = 39 },
                new Dog { Name = "Sandy", Color = "red & amber", TailLength = 212, Weight = 40 },
                new Dog { Name = "Adolf", Color = "red & amber", TailLength = 122, Weight = 41 },
                new Dog { Name = "Arthas", Color = "black", TailLength = 32, Weight = 42 },
                new Dog { Name = "Cleo", Color = "red & amber", TailLength = 30, Weight = 43 },
                new Dog { Name = "Nina", Color = "black", TailLength = 29, Weight = 44 },
                new Dog { Name = "Sam", Color = "red & amber", TailLength = 19, Weight = 45 },
                new Dog { Name = "Will", Color = "red & amber", TailLength = 20, Weight = 46 },
                new Dog { Name = "Bessy", Color = "black", TailLength = 22, Weight = 47 }
            };

            context.Dogs.AddRange(dogs);
            context.SaveChanges();
        }
    }
}
