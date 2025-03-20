using CliniCare.Application.Abstractions;
using CliniCare.Application.Helpers;
using CliniCare.Application.InputModels.Client;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Models;
using CliniCare.Domain.Repositories;
using MediatR;
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
        private readonly IApplicationUser _user;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(
            IClientRepository clientRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService,
            IUnitOfWork unitOfWork,
            IApplicationUser user)
        {
            _clientRepository = clientRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<Result<Unit>> CreateClientAsync(CreateClientInputModel createClientInputModel)
        {
            var existingUser = await _userManager.FindByEmailAsync(createClientInputModel.Email);
            if (existingUser != null)
            {
                return Result<Unit>.Failure("E-mail já está em uso!");
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
                return Result<Unit>.Failure(errors);
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
                    return Result<Unit>.Failure("Erro ao salvar os dados.");
                }

                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure($"Erro: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<ClientViewModel>>> GetAllClientsAsync()
        {

            var user = await _clientRepository.GetAllClientAsync();
            if (!user.Any())
            {
                return Result<IEnumerable<ClientViewModel>>.Failure("Nenhum cliente para consultar");
            }

            var clients = user.Select(c => new ClientViewModel
            {
                Id = c.Id,
                CPF = c.CPF,
                Name = c.Name,
                Address = c.Address,
                Email = c.ApplicationUser.Email,
                Phone = c.ApplicationUser.PhoneNumber,
            });

            return Result<IEnumerable<ClientViewModel>>.Success(clients);

        }

        public async Task<Result<ClientViewModel>> GetClientByIdAsync(int id)
        {
            var user = await _clientRepository.GetClientByIdAsync(id);
            if (user == null)
            {
                return Result<ClientViewModel>.Failure("Nenhum cliente encontrado com esse ID");
            }

            var client = new ClientViewModel
            {
                Id = id,
                Address = user.Address,
                Email = user.ApplicationUser.Email,
                Phone = user.ApplicationUser.PhoneNumber,
                CPF = user.CPF,
            };

            return Result<ClientViewModel>.Success(client);
        }

        public async Task<Result<ClientViewModel>> GetProfileClientAsync()
        {
            var userId = _user.Id;

            var userLogado = await _clientRepository.GetCurrentClientAsync(userId);
            if (userLogado == null)
            {
                return Result<ClientViewModel>.Failure("Usuário não encontrado.");

            }

            var client = new ClientViewModel
            {
                Id = userLogado.Id,
                Name = userLogado.Name,
                Email = userLogado.ApplicationUser.Email,
                Phone = userLogado.ApplicationUser.PhoneNumber,
                CPF = userLogado.CPF,
                Address = userLogado.Address
            };

            return Result<ClientViewModel>.Success(client);


        }

        public async Task<Result<string>> LoginClientAsync(LoginClientInputModel loginClientInputModel)
        {
            var user = await _userManager.FindByEmailAsync(loginClientInputModel.Email);
            if (user == null)
            {
                return Result<string>.Failure("E-mail para login inválido!");
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginClientInputModel.Password, false, false);
            if (!result.Succeeded)
            {
                return Result<string>.Failure("Tentativa de login inválida.");
            }
            var role = await _userManager.GetRolesAsync(user);
            var token = await _jwtService.GerarJwt(user, role);

            return Result<string>.Success(token);
        }

        public async Task<Result<Unit>> UpdateClientAsync(UpdateClientInputModel updateClientInputModel)
        {
            var userId = _user.Id;

            var userLogado = await _clientRepository.GetCurrentClientAsync(userId);
            if (userLogado == null)
            {
                return Result<Unit>.Failure("Cliente não encontrado!");
            }

           
            userLogado.Name = updateClientInputModel.Name;
            userLogado.Address = updateClientInputModel.Address;


            _unitOfWork.Clients.UpdateClient(userLogado);
            var updatedClient = await _unitOfWork.CommitAsync();

            if (!updatedClient)
            {
                return Result<Unit>.Failure("Não foi possível realizar modificações.");
            }

            return Result<Unit>.Success(Unit.Value);

        }

        public async Task<Result<Unit>> UpdateClientByAdminAsync(int clientId, UpdateClientInputModel updateClientInputModel)
        {
            var user =  await _clientRepository.GetClientByIdAsync(clientId);
            if (user == null)
            {
                return Result<Unit>.Failure("Não foi encontrado nenhum cliente com esse id.");
            }

            user.Address = updateClientInputModel.Address;
            user.Name = updateClientInputModel.Name;

            _unitOfWork.Clients.UpdateClient(user);
           var updatedClient = await _unitOfWork.CommitAsync();

            if (!updatedClient)
            {
                return Result<Unit>.Failure("Falha ao atualizar cliente!");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
