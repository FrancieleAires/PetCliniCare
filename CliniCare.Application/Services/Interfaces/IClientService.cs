using CliniCare.Application.Helpers;
using CliniCare.Application.InputModels.Client;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<Result<Unit>> CreateClientAsync(CreateClientInputModel createClientInputModel);
        Task<Result<string>> LoginClientAsync(LoginClientInputModel loginClientInputModel);
        Task<Result<Unit>> UpdateClientAsync(UpdateClientInputModel updateClientInputModel);
        Task<Result<Unit>> UpdateClientByAdminAsync(int clientId, UpdateClientInputModel updateClientInputModel);
        Task<Result<IEnumerable<ClientViewModel>>> GetAllClientsAsync();
        Task<Result<ClientViewModel>> GetProfileClientAsync();
        Task<Result<ClientViewModel>> GetClientByIdAsync(int id);

    }
}
