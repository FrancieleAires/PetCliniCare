using CliniCare.Domain.Models;
using CliniCare.Domain.Repositories;
using CliniCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Infrastructure.Repository
{
    public class HospitalizationRepository : IHospitalizationRepository
    {
        private readonly ApiDbContext _dbContext;

        public HospitalizationRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Hospitalization?> GetByIdAsync(int id)
        {
            return await _dbContext.Hospitalizations
                .Include(h => h.Animal)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Hospitalization>> GetByAnimalIdAsync(int animalId)
        {
            return await _dbContext.Hospitalizations
                .Where(h => h.AnimalId == animalId)
                .ToListAsync();
        }

        public async Task AddAsync(Hospitalization hospitalization)
        {
            await _dbContext.Hospitalizations.AddAsync(hospitalization);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hospitalization hospitalization)
        {
            _dbContext.Hospitalizations.Update(hospitalization);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hospitalization = await GetByIdAsync(id);
            if (hospitalization != null)
            {
                _dbContext.Hospitalizations.Remove(hospitalization);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
