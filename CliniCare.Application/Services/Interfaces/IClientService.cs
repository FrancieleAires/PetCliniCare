using CliniCare.Application.Helpers;
using CliniCare.Application.InputModels.Client;
using CliniCare.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<Result> CreateClientAsync(CreateClientInputModel createClientInputModel);
        Task<Result> LoginClientAsync(LoginClientInputModel loginClientInputModel);
        Task<Result> UpdateClientAsync(UpdateClientInputModel updateClientInputModel);
        Task<Result> UpdateClientByAdminAsync(int clientId, UpdateClientInputModel updateClientInputModel);
        Task<IEnumerable<ClientViewModel>> GetAllClientsAsync();
        Task<ClientViewModel> GetProfileClientAsync();
        Task<ClientViewModel> GetClientByIdAsync(int id);

    }
}
