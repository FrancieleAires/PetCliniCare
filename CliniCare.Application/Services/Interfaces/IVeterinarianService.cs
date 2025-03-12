using CliniCare.Application.Helpers;
using CliniCare.Application.InputModels.Client;
using CliniCare.Application.InputModels.Veterinarian;
using CliniCare.Application.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CliniCare.Application.Services.Interfaces
{
    public interface IVeterinarianService
    {
        Task<Result<Unit>> CreateVeterinarianAsync(CreateVeterinarianInputModel createVeterinarianInputModel);
        Task<Result<string>> LoginVeterinarianAsync(LoginVeterinarianInputModel loginVeterinarianInputModel);
        Task<Result<Unit>> UpdateVeterinarianAsync(int veterinarianId, UpdateVeterinarianInputModel updateVeterinarianInputModel);
        Task<Result<IEnumerable<VeterinarianViewModel>>> GetAllVeterinariansAsync();
        Task<Result<VeterinarianViewModel>> GetVeterinarianByIdAsync(int id);
        Task<Result<Unit>> DeleteVeterinarianAsync(int id);
    }
}
