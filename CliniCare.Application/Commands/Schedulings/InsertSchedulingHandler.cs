using CliniCare.Application.Helpers;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Domain.Enums;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Schedulings
{
    public class InsertSchedulingHandler: IRequestHandler<InsertSchedulingCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUser _user;

        public InsertSchedulingHandler(IUnitOfWork unitOfWork, IApplicationUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<Result<Unit>> Handle(InsertSchedulingCommand request, CancellationToken cancellationToken)
        {
            var userId = _user.Id;
            
            var client = await _unitOfWork.Clients.GetClientByUserIdAsync(userId);
            if (client == null) return Result<Unit>.Failure("Não foi encontrado nenhum usuário autenticado.");

            
            var scheduling = request.ToEntity(client.Id);

            await _unitOfWork.Schedulings.AddSchedulingAsync(scheduling);
            var result = await _unitOfWork.CommitAsync();

            if(!result)
            {
                return Result<Unit>.Failure("Erro ao cadastrar um agendamento!");
            }

            return Result<Unit>.Success(Unit.Value);


        }
    }
}
