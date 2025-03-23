using CliniCare.Application.Helpers;
using CliniCare.Domain.Enums;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Schedulings
{
    public class CancelSchedulingHandler : IRequestHandler<CancelSchedulingCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelSchedulingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(CancelSchedulingCommand request, CancellationToken cancellationToken)
        {
            var scheduling = await _unitOfWork.Schedulings.GetSchedulingByIdAsync(request.SchedulingId);
            if (scheduling == null) return Result<Unit>.Failure("Agendamento não encontrado.");

            if (scheduling.SchedulingsStatus == SchedulingsStatus.Finalizado)
            {
                return Result<Unit>.Failure("Não é possível cancelar um agendamento finalizado.");
            }

            if (scheduling.SchedulingsStatus == SchedulingsStatus.Cancelado)
            {
                return Result<Unit>.Failure("O agendamento já está cancelado.");
            }

            scheduling.SchedulingsStatus = SchedulingsStatus.Cancelado;
            await _unitOfWork.CommitAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
