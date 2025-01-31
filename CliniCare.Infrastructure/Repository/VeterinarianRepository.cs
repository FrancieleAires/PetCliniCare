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
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteVeterinarianAsync(Veterinarian veterinarian)
        {
             _dbContext.Veterinarians.Remove(veterinarian);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Veterinarian>> GetAllVeterinarianAsync()
        {
            return await _dbContext.Veterinarians.ToListAsync();
        }

        public async Task<Veterinarian> GetVeterinarianByIdAsync(int id)
        {
            return await _dbContext.Veterinarians.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task UpdateVeterinarianAsync(Veterinarian veterinarian)
        {
            
             _dbContext.Veterinarians.Update(veterinarian);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Veterinarians.AnyAsync(c => c.Id == id);
        }
    }
}

