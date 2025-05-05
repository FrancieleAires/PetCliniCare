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

namespace CliniCare.Application.Commands.Animals.DeleteAnimal
{
    public class DeleteAnimalHandler : IRequestHandler<DeleteAnimalCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUser _user;
        public DeleteAnimalHandler(IUnitOfWork unitOfWork, IApplicationUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<Result<Unit>> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
        {
            var userId = _user.Id;

            var client = await _unitOfWork.Clients.GetClientByUserIdAsync(userId);
            if (client == null) return Result<Unit>.Failure("Cliente não encontrado.");
            var animal = await _unitOfWork.Animals.GetAnimalByIdAsync(request.AnimalId);
            if (animal == null) return Result<Unit>.Failure("Pet não encontrado.");
            if (animal.ClientId != client.Id) return Result<Unit>.Failure("Você não tem permissão para deletar este pet.");

            await _unitOfWork.Animals.DeleteAnimalAsync(request.AnimalId);
            var result = await _unitOfWork.CommitAsync();
            if (!result) return Result<Unit>.Failure("Erro ao deletar o pet. Por favor, tente novamente ou entre em contato com o suporte.");

            return Result<Unit>.Success(Unit.Value);


        }
    }
}
