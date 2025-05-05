using CliniCare.Application.Helpers;
using CliniCare.Domain.Enums;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Schedulings.FinalizeScheduling
{
    public class FinalizeSchedulingHandler : IRequestHandler<FinalizeSchedulingCommand, Result<Unit>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public FinalizeSchedulingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(FinalizeSchedulingCommand request, CancellationToken cancellationToken)
        {
            var scheduling = await _unitOfWork.Schedulings.GetSchedulingByIdAsync(request.SchedulingId);

            if (scheduling.SchedulingsStatus != SchedulingsStatus.Confirmado)
            {
                return Result<Unit>.Failure("Não foi possível finalizar este agendamento.");
            }

            scheduling.SchedulingsStatus = SchedulingsStatus.Finalizado;

            await _unitOfWork.Schedulings.UpdateSchedulingAsync(scheduling);
            var result = await _unitOfWork.CommitAsync();

            if (!result) Result<Unit>.Failure("Não foi possível confirmar este agendamento.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
