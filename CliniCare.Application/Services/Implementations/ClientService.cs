using CliniCare.Application.Abstractions;
using CliniCare.Application.Helpers;
using CliniCare.Application.InputModels.Client;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Models;
using CliniCare.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(
            IClientRepository clientRepository,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor, 
            IJwtService jwtService,
            IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateClientAsync(CreateClientInputModel createClientInputModel)
        {
            var existingUser = await _userManager.FindByEmailAsync(createClientInputModel.Email);
            if (existingUser != null)
            {
                return Result.Failure("E-mail já está em uso!");
            }

            var user = new ApplicationUser
            {
                UserName = createClientInputModel.Email,
                Email = createClientInputModel.Email,
                PhoneNumber = createClientInputModel.Phone,
            };

            var result = await _userManager.CreateAsync(user, createClientInputModel.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result.Failure(errors);
            }
            await _userManager.AddToRoleAsync(user, "Client");

            var client = new Client
            {
                Name = createClientInputModel.Name,
                CPF = createClientInputModel.CPF,
                Address = createClientInputModel.Address,
                ApplicationUserId = user.Id
            };

            try
            {
                await _unitOfWork.Clients.AddClientAsync(client);
                var success = await _unitOfWork.CommitAsync();
                if (!success)
                {
                    return Result.Failure("Erro ao salvar os dados.");
                }

                return Result.Success("Cliente registrado com sucesso!");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Erro: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ClientViewModel>> GetAllClientsAsync()
        {

            var user = await _clientRepository.GetAllClientAsync();
            if (!user.Any())
            {
                return Enumerable.Empty<ClientViewModel>();
            }

            return user.Select(c => new ClientViewModel
            {
                Id = c.Id,
                CPF = c.CPF,
                Address = c.Address,
                Email = c.ApplicationUser.Email,
                Phone = c.ApplicationUser.PhoneNumber,
            });

        }

        public async Task<ClientViewModel> GetClientByIdAsync(int id)
        {
            var user = await _clientRepository.GetClientByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return new ClientViewModel
            {
                Id = id,
                Address = user.Address,
                Email = user.ApplicationUser.Email,
                Phone = user.ApplicationUser.PhoneNumber,
                CPF = user.CPF,
            };
        }

        public async Task<ClientViewModel> GetProfileClientAsync()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault
                (c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return null; 
            }
            if (int.TryParse(userIdString, out var userId))
            {
                var userLogado = await _clientRepository.GetCurrentClientAsync(userId);
                if (userLogado == null)
                {
                    return null;
                }

                var clientViewModel = new ClientViewModel
                {
                    Id = userId,
                    Name = userLogado.Name,
                    Email = userLogado.ApplicationUser.Email,
                    Phone = userLogado.ApplicationUser.PhoneNumber,
                    CPF = userLogado.CPF,
                    Address = userLogado.Address
                };

                return clientViewModel;
            }
            else
            {
                return null;
            }

        }

        public async Task<Result> LoginClientAsync(LoginClientInputModel loginClientInputModel)
        {
            var user = await _userManager.FindByEmailAsync(loginClientInputModel.Email);
            if (user == null)
            {
                return Result.Failure("E-mail para login inválido!");
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginClientInputModel.Password, false, false);
            if (!result.Succeeded)
            {
                return Result.Failure("Tentativa de login inválida.");
            }
            var role = await _userManager.GetRolesAsync(user);
            var token = await _jwtService.GerarJwt(user, role);

            return Result.Success(token);
        }

        public async Task<Result> UpdateClientAsync(UpdateClientInputModel updateClientInputModel)
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.Claims
         .FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return Result.Failure("Usuário não autenticado!");
            }

            if (!int.TryParse(userIdString, out var userId))
            {
                return Result.Failure("ID do usuário inválido!");
            }

            var userLogado = await _clientRepository.GetCurrentClientAsync(userId);
            if (userLogado == null)
            {
                return Result.Failure("Cliente não encontrado!");
            }

           
            userLogado.Name = updateClientInputModel.Name;
            userLogado.Address = updateClientInputModel.Address;


            _unitOfWork.Clients.UpdateClient(userLogado);
            var updatedClient = await _unitOfWork.CommitAsync();

            if (!updatedClient)
            {
                return Result.Failure("Não foi possível realizar modificações.");
            }

            return Result.Success("Cliente atualizado com sucesso!");

        }

        public async Task<Result> UpdateClientByAdminAsync(int clientId, UpdateClientInputModel updateClientInputModel)
        {
            var user =  await _clientRepository.GetClientByIdAsync(clientId);
            if (user == null)
            {
                return Result.Failure("Não foi encontrado nenhum cliente com esse id.");
            }

            user.Address = updateClientInputModel.Address;
            user.Name = updateClientInputModel.Name;

            _unitOfWork.Clients.UpdateClient(user);
           var updatedClient = await _unitOfWork.CommitAsync();

            if (!updatedClient)
            {
                return Result.Failure("Falha ao atualizar cliente!");
            }

            return Result.Success("Cliente atualizado com sucesso!.");
        }
    }
}
