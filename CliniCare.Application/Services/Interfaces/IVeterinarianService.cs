using CliniCare.Application.Helpers;
using CliniCare.Application.InputModels.Client;
using CliniCare.Application.InputModels.Veterinarian;
using CliniCare.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Services.Interfaces
{
    public interface IVeterinarianService
    {
        Task<Result> CreateVeterinarianAsync(CreateVeterinarianInputModel createVeterinarianInputModel);
        Task<Result> LoginVeterinarianAsync(LoginVeterinarianInputModel loginVeterinarianInputModel);
        Task<Result> UpdateVeterinarianAsync(int veterinarianId, UpdateVeterinarianInputModel updateVeterinarianInputModel);
        Task<IEnumerable<VeterinarianViewModel>> GetAllClientsAsync();
        Task<VeterinarianViewModel> GetProfileVeterinarianAsync();
        Task<VeterinarianViewModel> GetVeterinariantByIdAsync(int id);
    }
}
