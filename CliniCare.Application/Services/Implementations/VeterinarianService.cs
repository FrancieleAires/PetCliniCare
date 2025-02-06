using CliniCare.Application.Abstractions;
using CliniCare.Application.Helpers;
using CliniCare.Application.InputModels.Veterinarian;
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
    public class VeterinarianService : IVeterinarianService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IVeterinarianRepository _veterinarianRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public VeterinarianService(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor contextAccessor,
            IJwtService jwtService,
            IVeterinarianRepository veterinarianRepository,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _jwtService = jwtService; 
            _veterinarianRepository = veterinarianRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<Result> CreateVeterinarianAsync(CreateVeterinarianInputModel createVeterinarianInputModel)
        {
            var userExisting = await _userManager.FindByEmailAsync(createVeterinarianInputModel.Email);
            if (userExisting != null)
            {
                return Result.Failure("E-mail já está em uso!");
            }

            var user = new ApplicationUser
            {
                Email = createVeterinarianInputModel.Email,
                UserName = createVeterinarianInputModel.Email
            };

            var result = await _userManager.CreateAsync(user, createVeterinarianInputModel.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result.Failure(errors);
            }
            await _userManager.AddToRoleAsync(user, "Admin");

            var veterinarian = new Veterinarian
            {
                Name = createVeterinarianInputModel.Name,
                CRMV = createVeterinarianInputModel.CRMV,
                Specialty = createVeterinarianInputModel.Specialty,
                ApplicationUserId = user.Id,
            };

            try
            {
                await _unitOfWork.Veterinarians.AddVeterinarianAsync(veterinarian);
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

        public async Task<IEnumerable<VeterinarianViewModel>> GetAllClientsAsync()
        {
            var user = await _veterinarianRepository.GetAllVeterinarianAsync();
            if (!user.Any())
            {
                return Enumerable.Empty<VeterinarianViewModel>();
            }

            return user.Select(c => new VeterinarianViewModel
            {
                Id = c.Id,
                Specialty = c.Specialty,
                CRMV = c.CRMV,
                Name = c.Name,
                Email = c.ApplicationUser.Email,
            });
        }

        public async Task<VeterinarianViewModel> GetClientByIdAsync(int id)
        {
            var user = await _veterinarianRepository.GetVeterinarianByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return new VeterinarianViewModel
            {
                Id = id,
                CRMV = user.CRMV,
                Name = user.Name,
                Specialty = user.Specialty,
                Email = user.ApplicationUser.Email,
            };
        }

        public async Task<VeterinarianViewModel> GetProfileVeterinarianAsync()
        {
            var userIdString = _contextAccessor.HttpContext?.User?.Claims.FirstOrDefault
                (c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return null;
            }
            if (int.TryParse(userIdString, out var userId))
            {
                var userLogado = await _veterinarianRepository.GetCurrentVeterinarianAsync(userId);
                if (userLogado == null)
                {
                    return null;
                }

                var veterinarianViewModel = new VeterinarianViewModel
                {
                    Name = userLogado.Name,
                    Specialty = userLogado.Specialty,
                    Email = userLogado.ApplicationUser.Email,
                };

                return veterinarianViewModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<VeterinarianViewModel> GetVeterinariantByIdAsync(int id)
        {
            var user = await _veterinarianRepository.GetVeterinarianByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return new VeterinarianViewModel
            {
                Name = user.Name,
                Specialty = user.Specialty,
                CRMV = user.CRMV,
                Email = user.ApplicationUser.Email,
            };
        }

        public async Task<Result> LoginVeterinarianAsync(LoginVeterinarianInputModel loginVeterinarianInputModel)
        {
            var user = await _userManager.FindByEmailAsync(loginVeterinarianInputModel.Email);
            if(user == null)
            {
                return Result.Failure("E-mail inválido para login.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVeterinarianInputModel.Password, false, false);
            if (!result.Succeeded)
            {
                return Result.Failure("Tentativa de login inválida.");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = await _jwtService.GerarJwt(user, roles);

            return Result.Success(token);
        }

        public async Task<Result> UpdateVeterinarianAsync(int veterinarianId, UpdateVeterinarianInputModel updateVeterinarianInputModel)
        {
            var user = await _veterinarianRepository.GetVeterinarianByIdAsync(veterinarianId);

            if(user == null)
            {
                return Result.Failure("Não foi encontrado nenhum cliente com esse id.");
            }

            user.Name = updateVeterinarianInputModel.Name;
            user.Specialty = updateVeterinarianInputModel.Specialty;
            _unitOfWork.Veterinarians.UpdateVeterinarian(user);
            var success = await _unitOfWork.CommitAsync();
            if (!success)
            {
                return Result.Failure("Falha ao atualizar cliente!");
            }

            return Result.Success("Cliente atualizado com sucesso!.");
        }
    }
}
