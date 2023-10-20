using BridgeDogs.Models;

namespace BridgeDogs.Interfaces
{
    public interface IDogRepository
    {
        Task<IEnumerable<Dog>> GetAllDogsAsync(string attribute = "name", string order = "asc", int pageNumber = 1, int pageSize = 10);

        Task<Dog> CreateDogAsync(Dog dog);

        Task<bool> DogExistsAsync(string name);
    }
}
