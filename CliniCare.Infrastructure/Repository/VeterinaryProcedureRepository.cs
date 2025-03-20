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
    public class VeterinaryProcedureRepository : IVeterinaryProcedureRepository
    {
        private readonly ApiDbContext _dbContext;

        public VeterinaryProcedureRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Procedure veterinaryProcedure)
        {
            await _dbContext.VeterinaryProcedures.AddAsync(veterinaryProcedure);
        }

        public async Task DeleteVeterinaryProcedureAsync(Procedure veterinaryProcedure)
        {
            _dbContext.VeterinaryProcedures.Remove(veterinaryProcedure);
        }

        public async Task<IEnumerable<Procedure>> GetAllVeterinaryProcedureAsync()
        {
            return await _dbContext.VeterinaryProcedures.
                ToListAsync();
        }

        public async Task<Procedure> GetByIdAsync(int id)
        {
            return await _dbContext.VeterinaryProcedures.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task UpdateAsync(Procedure veterinaryProcedure)
        {
            _dbContext.VeterinaryProcedures.Update(veterinaryProcedure);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.VeterinaryCares.AnyAsync(c => c.Id == id);
        }
    }
}
