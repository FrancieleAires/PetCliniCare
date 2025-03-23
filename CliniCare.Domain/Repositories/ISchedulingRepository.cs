using CliniCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Repositories
{
    public interface ISchedulingRepository
    {
        Task AddSchedulingAsync(Scheduling scheduling);
        Task UpdateSchedulingAsync(Scheduling scheduling);
        Task<IEnumerable<Scheduling>> GetAllSchedulingAsync();
        Task<Scheduling> GetSchedulingByIdAsync(int id);
        Task DeleteSchedulingAsync(Scheduling scheduling);
        Task<IEnumerable<Scheduling>> GetAllSchedulingsByClientIdAsync(int clientId);
    }
}
