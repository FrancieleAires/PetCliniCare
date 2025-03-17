using CliniCare.Application.Helpers;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Animals
{
    public class UpdateAnimalHandler : IRequestHandler<UpdateAnimalCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUser _user;


        public UpdateAnimalHandler(IUnitOfWork unitOfWork, IApplicationUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }
        public async Task<Result<Unit>> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
        {
            var userId = _user.Id;
            var client = await _unitOfWork.Clients.GetClientByUserIdAsync(userId);
            if (client == null)
            {
                return Result<Unit>.Failure("Cliente não encontrado para este usuário.");
            }
            var animal = await _unitOfWork.Animals.GetAnimalByClientIdAsync(request.Id);
            if (animal == null) return Result<Unit>.Failure("Nenhum animal encontrado para este usuário.");

            if (animal.ClientId != client.Id)
            {
                return Result<Unit>.Failure("Você não tem permissão para atualizar este animal.");
            }

            animal.Species = request.Species;
            animal.Name = request.Name;
            animal.Breed = request.Breed;
            animal.DateOfBirth = request.DateOfBirth;
            animal.Gender = request.Gender; 
            

            await _unitOfWork.Animals.UpdateAnimalAsync(animal);
            var result = await _unitOfWork.CommitAsync();
            if(!result) return Result<Unit>.Failure("Erro ao atualizar os dados.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
