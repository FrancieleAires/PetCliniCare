using CliniCare.Application.Helpers;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Animals
{
    public class InsertAnimalHandler : IRequestHandler<InsertAnimalCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUser _user;

        public InsertAnimalHandler(IUnitOfWork unitOfWork, IApplicationUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<Result<Unit>> Handle(InsertAnimalCommand request, CancellationToken cancellationToken)
        {
            var userId = _user.Id;

            

            var client = await _unitOfWork.Clients.GetClientByUserIdAsync(userId);
            if (client == null)
            {
                return Result<Unit>.Failure("Cliente não encontrado.");
            }

            var animal = request.ToEntity(client.Id);
            await _unitOfWork.Animals.AddAnimalAsync(animal);
            var result = await _unitOfWork.CommitAsync();

            if (!result)
            {
                return Result<Unit>.Failure("Erro ao salvar os dados.");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
