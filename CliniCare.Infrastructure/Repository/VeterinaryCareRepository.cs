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
    public class VeterinaryCareRepository : IVeterinaryCareRepository
    {
        private readonly ApiDbContext _dbContext;

        public VeterinaryCareRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddVeterinaryCareAsync(VeterinaryCare veterinaryCare)
        {
            await _dbContext.VeterinaryCares.AddAsync(veterinaryCare);
            
        }

        public async Task DeleteVeterinaryCareAsync(VeterinaryCare veterinaryCare)
        {
            _dbContext.VeterinaryCares.Remove(veterinaryCare);
            
        }

        public async Task<IEnumerable<VeterinaryCare>> GetAllVeterinaryCareAsync()
        {
            return await _dbContext.VeterinaryCares
             .Include(s => s.Veterinarian)
             .Include(s => s.Animal)
             .Include(s => s.Scheduling)
             .ToListAsync();
        }

        public async Task<VeterinaryCare?> GetCareByIdAsync(int id)
        {
            return await _dbContext.VeterinaryCares
        .Include(v => v.Veterinarian) 
        .Include(v => v.Animal)       
        .Include(v => v.Scheduling)   
        .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task UpdateVeterinaryCareAsync(VeterinaryCare veterinaryCare)
        {
            _dbContext.VeterinaryCares.Update(veterinaryCare);
            
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.VeterinaryCares.AnyAsync(c => c.Id == id);
        }
    }
}
