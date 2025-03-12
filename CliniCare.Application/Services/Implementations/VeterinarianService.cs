using CliniCare.Application.Abstractions;
using CliniCare.Application.Helpers;
using CliniCare.Application.InputModels.Veterinarian;
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

        public async Task<Result<Unit>> CreateVeterinarianAsync(CreateVeterinarianInputModel createVeterinarianInputModel)
        {
            var userExisting = await _userManager.FindByEmailAsync(createVeterinarianInputModel.Email);
            if (userExisting != null)
            {
                return Result<Unit>.Failure("E-mail já está em uso!");
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
                return Result<Unit>.Failure(errors);
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
                    return Result<Unit>.Failure("Erro ao salvar os dados.");
                }

                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure($"Erro: {ex.Message}");
            }
        }

        public async Task<Result<Unit>> DeleteVeterinarianAsync(int id)
        {
            var veterinarian = await _veterinarianRepository.GetVeterinarianByIdAsync(id);
            if (veterinarian == null)
            {
                return Result<Unit>.Failure("Não foi possível encontrar um veterinário com esse ID.");
            }

            
            var deleteResult = await _veterinarianRepository.DeleteVeterinarianAsync(id);

            if (deleteResult != null)
            {
                if (veterinarian.ApplicationUser != null)
                {
                    var userDeleteResult = await _userManager.DeleteAsync(veterinarian.ApplicationUser);
                    if (userDeleteResult.Succeeded)
                    {
                        return Result<Unit>.Success(Unit.Value);
                    }
                    else
                    {
                        return Result<Unit>.Failure("Veterinário deletado, mas não foi possível excluir o usuário.");
                    }
                }

                return Result<Unit>.Success(Unit.Value);
            }
            else
            {
                return Result<Unit>.Failure("Não foi possível excluir o veterinário");
            }
        }

        public async Task<Result<IEnumerable<VeterinarianViewModel>>> GetAllVeterinariansAsync()
        {
            var users = await _veterinarianRepository.GetAllVeterinarianAsync();

            if (!users.Any())
            {
                return Result<IEnumerable<VeterinarianViewModel>>.Failure("Nenhum veterinário encontrado.");
            }

            var veterinarians = users.Select(c => new VeterinarianViewModel
            {
                Id = c.Id,
                Specialty = c.Specialty,
                CRMV = c.CRMV,
                Name = c.Name,
                Email = c.ApplicationUser.Email,
            });

            return Result<IEnumerable<VeterinarianViewModel>>.Success(veterinarians);
        }

        public async Task<Result<VeterinarianViewModel>> GetVeterinarianByIdAsync(int id)
        {
            var user = await _veterinarianRepository.GetVeterinarianByIdAsync(id);
            if (user == null)
            {
                return Result<VeterinarianViewModel>.Failure("Não foi possível consultar o veterinário pelo ID fornecido.");
            }

            var veterinarianViewModel = new VeterinarianViewModel
            {
                Id = id,
                Name = user.Name,
                Specialty = user.Specialty,
                CRMV = user.CRMV,
                Email = user.ApplicationUser.Email,
            };

            return Result<VeterinarianViewModel>.Success(veterinarianViewModel);
        }

        public async Task<Result<string>> LoginVeterinarianAsync(LoginVeterinarianInputModel loginVeterinarianInputModel)
        {
            var user = await _userManager.FindByEmailAsync(loginVeterinarianInputModel.Email);
            if(user == null)
            {
                return Result<string>.Failure("E-mail inválido para login.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVeterinarianInputModel.Password, false, false);
            if (!result.Succeeded)
            {
                return Result<string>.Failure("Tentativa de login inválida.");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = await _jwtService.GerarJwt(user, roles);

            return Result<string>.Success(token);
        }

        public async Task<Result<Unit>> UpdateVeterinarianAsync(int veterinarianId, UpdateVeterinarianInputModel updateVeterinarianInputModel)
        {
            var user = await _veterinarianRepository.GetVeterinarianByIdAsync(veterinarianId);

            if(user == null)
            {
                return Result<Unit>.Failure("Não foi encontrado nenhum cliente com esse id.");
            }

            user.Name = updateVeterinarianInputModel.Name;
            user.Specialty = updateVeterinarianInputModel.Specialty;
            _unitOfWork.Veterinarians.UpdateVeterinarian(user);
            var success = await _unitOfWork.CommitAsync();
            if (!success)
            {
                return Result<Unit>.Failure("Falha ao atualizar cliente!");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
