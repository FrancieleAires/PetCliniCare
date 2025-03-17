using CliniCare.Domain.Repositories;
using CliniCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliniCare.Infrastructure.Persistence;

namespace CliniCare.Infrastructure.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ApiDbContext _dbContext;

        public AnimalRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAnimalAsync(Animal animal)
        {
            await _dbContext.Animals.AddAsync(animal);

        }

        public async Task<Animal> DeleteAnimalAsync(int id)
        {
             var animals = await _dbContext.Animals
            .Include(a => a.Client)
            .FirstOrDefaultAsync(a => a.Id == id);
            _dbContext.Animals.Remove(animals);
            return animals;

        }

        public async Task<IEnumerable<Animal>> GetAllAnimalAsync(int clientId)
        {
            return await _dbContext.Animals
                .Where(a => a.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<Animal> GetAnimalByClientIdAsync(int id)
        {
            return await _dbContext.Animals
                .Include(c => c.Client)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            return await _dbContext.Animals.FirstOrDefaultAsync(a => a.Id == id);
                
        }
        public async Task UpdateAnimalAsync(Animal animal)
        {
            _dbContext.Animals.Update(animal);
            
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Animals.AnyAsync(c => c.Id == id);
        }
    }
}
