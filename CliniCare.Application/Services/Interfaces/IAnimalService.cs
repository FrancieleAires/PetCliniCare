using CliniCare.Application.Helpers;
using CliniCare.Application.InputModels.Animal;
using CliniCare.Application.InputModels.Veterinarian;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<Result<Unit>> CreateAnimalAsync(CreateAnimalInputModel createAnimalInputModel);
        Task<Result<Unit>> UpdateAnimalAsync(int animalId, UpdateAnimalInputModel updateAnimalInputModel);
        Task<IEnumerable<VeterinarianViewModel>> GetAllClientsAsync();
        Task<Result<VeterinarianViewModel>> GetVeterinariantByIdAsync(int id);
        Task<Result<Unit>> DeleteVeterinarianAsync(int id);

    }
}
