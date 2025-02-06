using CliniCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Domain.Repositories
{
    public interface IVeterinarianRepository
    {
        Task AddVeterinarianAsync(Veterinarian veterinarian);
        void UpdateVeterinarian(Veterinarian veterinarian);
        Task<IEnumerable<Veterinarian>> GetAllVeterinarianAsync();
        Task<Veterinarian> GetCurrentVeterinarianAsync(int userId);
        Task<Veterinarian> GetVeterinarianByIdAsync(int id);
        Task DeleteVeterinarianAsync(Veterinarian veterinarian);
        Task<bool> ExistsAsync(int id);


    }
}
