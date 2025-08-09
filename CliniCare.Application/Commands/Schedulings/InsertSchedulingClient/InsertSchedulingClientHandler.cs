using CliniCare.Application.Helpers;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Domain.Constants;
using CliniCare.Domain.Enums;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Schedulings.InsertSchedulingClient
{
    public class InsertSchedulingClientHandler : IRequestHandler<InsertSchedulingClientCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUser _user;
        

        public InsertSchedulingClientHandler(IUnitOfWork unitOfWork, IApplicationUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<Result<Unit>> Handle(InsertSchedulingClientCommand request, CancellationToken cancellationToken)
        {
            var userId = _user.Id;
            var userRole = _user.Role;

            if (string.IsNullOrEmpty(userRole))
                return Result<Unit>.Failure("Não foi possível identificar o papel do usuário.");

            if(userRole != RoleNames.Client)
            {
                return Result<Unit>.Failure("Apenas usuários com o papel de Cliente podem cadastrar agendamentos.");
            }
            
            var existAnimal = await _unitOfWork.Animals.GetAnimalByClientIdAsync(request.AnimalId, userId);
            if(existAnimal == null)
            {
                return Result<Unit>.Failure("O animal informado não pertence ao cliente autenticado.");
            }

            var client = await _unitOfWork.Clients.GetCurrentClientAsync(userId);
            if (client == null) return Result<Unit>.Failure("Não foi encontrado nenhum usuário autenticado.");
            var scheduling = request.ToEntity(client.Id);

            await _unitOfWork.Schedulings.AddSchedulingAsync(scheduling);
            var result = await _unitOfWork.CommitAsync();

            if (!result)
            {
                return Result<Unit>.Failure("Erro ao cadastrar um agendamento!");
             }

            return Result<Unit>.Success(Unit.Value);


        }
    }
}
