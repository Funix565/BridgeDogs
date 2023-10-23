using BridgeDogs.Models;

namespace BridgeDogs.Interfaces
{
    public interface IDogRepository
    {
        Task<IEnumerable<Dog>> GetAllDogsAsync(DogParameters dogParameters);

        Task<Dog> CreateDogAsync(Dog dog);

        Task<bool> DogExistsAsync(string name);
    }
}
