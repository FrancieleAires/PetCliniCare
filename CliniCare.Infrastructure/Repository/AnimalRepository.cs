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

        public async Task DeleteAnimalAsync(Animal animal)
        {
            _dbContext.Animals.Remove(animal);
            
        }

        public async Task<IEnumerable<Animal>> GetAllAnimalAsync()
        {
            return await _dbContext.Animals.ToListAsync();
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
