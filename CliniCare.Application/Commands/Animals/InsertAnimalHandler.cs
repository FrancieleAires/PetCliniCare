using CliniCare.Application.Helpers;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InsertAnimalHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<Unit>> Handle(InsertAnimalCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Result<Unit>.Failure("Usuário não autenticado.");
            }

            if (!int.TryParse(userId, out int clientId))
            {
                return Result<Unit>.Failure("ID de usuário inválido.");
            }

            var client = await _unitOfWork.Clients.GetClientByUserIdAsync(clientId);
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
