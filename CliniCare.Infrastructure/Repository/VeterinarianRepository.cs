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
    public class VeterinarianRepository : IVeterinarianRepository
    {
        private readonly ApiDbContext _dbContext;

        public VeterinarianRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddVeterinarianAsync(Veterinarian veterinarian)
        {
            await _dbContext.Veterinarians.AddAsync(veterinarian);
        }

        public async Task<IEnumerable<Veterinarian>> GetAllVeterinarianAsync()
        {
            return await _dbContext.Veterinarians
                .Include(c => c.ApplicationUser)
                .ToListAsync();
        }

        public async Task<Veterinarian> GetVeterinarianByIdAsync(int id)
        {
            return await _dbContext.Veterinarians
            .Include(v => v.ApplicationUser)
            .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Veterinarian> DeleteVeterinarianAsync(int id)
        {
            var veterinarian = await _dbContext.Veterinarians
            .Include(v => v.ApplicationUser)
            .FirstOrDefaultAsync(v => v.Id == id);
            _dbContext.Veterinarians.Remove(veterinarian);
            return veterinarian;
        }

        public void UpdateVeterinarian(Veterinarian veterinarian)
        {
            
             _dbContext.Veterinarians.Update(veterinarian);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Veterinarians.AnyAsync(c => c.Id == id);
        }

        public async Task<Veterinarian> GetCurrentVeterinarianAsync(int userId)
        {
            return await _dbContext.Veterinarians
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
        }
    }
}

