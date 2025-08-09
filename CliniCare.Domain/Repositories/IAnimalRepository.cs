using CliniCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Repositories
{
    public interface IAnimalRepository
    {
        Task AddAnimalAsync(Animal animal);
        Task UpdateAnimalAsync(Animal animal);
        Task<IEnumerable<Animal>> GetAllAnimalsByClientAsync(int clientId);
        Task<List<Animal>> GetAllAnimalsAsync();
        Task<Animal?> GetAnimalByIdAsync(int id);
        Task<Animal?> GetAnimalByClientIdAsync(int id, int clientId);
        Task<Animal> DeleteAnimalAsync(int id);
        Task<bool> ExistsAsync(int id);
        
    }
}
