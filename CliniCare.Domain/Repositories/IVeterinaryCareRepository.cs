using CliniCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Repositories
{
    public interface IVeterinaryCareRepository
    {
        Task AddVeterinaryCareAsync(VeterinaryCare veterinaryCare);
        Task UpdateVeterinaryCareAsync(VeterinaryCare veterinaryCare);
        Task DeleteVeterinaryCareAsync(VeterinaryCare veterinaryCare);
        Task<IEnumerable<VeterinaryCare>> GetAllVeterinaryCareAsync();
        Task<VeterinaryCare> GetCareByIdAsync(int id);
    }
}
