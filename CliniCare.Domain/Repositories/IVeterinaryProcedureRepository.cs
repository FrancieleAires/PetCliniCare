using CliniCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Repositories
{
    public interface IVeterinaryProcedureRepository
    {
        Task AddAsync(Procedure veterinaryProcedure);
        Task UpdateAsync(Procedure veterinaryProcedure);
        Task DeleteVeterinaryProcedureAsync(Procedure veterinaryProcedure);
        Task<IEnumerable<Procedure>> GetAllVeterinaryProcedureAsync();
        Task<Procedure> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
