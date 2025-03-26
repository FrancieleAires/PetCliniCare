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
    public class SchedulingRepository : ISchedulingRepository
    {
        private readonly ApiDbContext _dbContext;

        public SchedulingRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddSchedulingAsync(Scheduling scheduling)
        {

            await _dbContext.Schedulings.AddAsync(scheduling);
        }

        public async Task DeleteSchedulingAsync(Scheduling scheduling)
        {
            _dbContext.Schedulings.Remove(scheduling);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Scheduling>> GetAllSchedulingAsync()
        {
            return await _dbContext.Schedulings
             .Include(s => s.Client)       
             .Include(s => s.Veterinarian) 
             .Include(s => s.Procedure)
             .Include(s => s.Animal)
             .ToListAsync();
        }
        public async Task<IEnumerable<Scheduling>> GetAllSchedulingsByClientIdAsync(int clientId)
        {
           
            return await _dbContext.Schedulings
                .Where(s => s.ClientId == clientId)
                .Include(s => s.Animal)  
                .Include(s => s.Veterinarian)
                .Include(s => s.Procedure)
                .Include(s => s.Client)   
                .ToListAsync();
        }
        public async Task<Scheduling?> GetSchedulingByIdAsync(int id)
        {
            return await _dbContext.Schedulings
                .Include(s => s.Animal)
                .Include(s => s.Veterinarian)
                .Include(s => s.Procedure)
                .Include(s => s.Client)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateSchedulingAsync(Scheduling scheduling)
        {
            _dbContext.Schedulings.Update(scheduling);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Schedulings.AnyAsync(c => c.Id == id);
        }
    }
}
