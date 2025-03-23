using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Schedulings
{
    public class GetSchedulingByIdHandler : IRequestHandler<GetSchedulingByIdQuery, Result<SchedulingViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSchedulingByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SchedulingViewModel>> Handle(GetSchedulingByIdQuery query, CancellationToken cancellationToken)
        {
            var scheduling = await _unitOfWork.Schedulings.GetSchedulingByIdAsync(query.SchedulingId);

            if (scheduling == null)
            {
                return Result<SchedulingViewModel>.Failure("Agendamento não encontrado.", ErrorType.NotFound);
            }

            var result = new SchedulingViewModel
            {
                Id = scheduling.Id,
                SchedulingDate = scheduling.SchedulingDate,
                SchedulingTime = scheduling.SchedulingTime,
                Observation = scheduling.Observation,
                SchedulingsStatus = scheduling.SchedulingsStatus,
                AnimalName = scheduling.Animal.Name,
                VeterinarianName = scheduling.Veterinarian.Name,
                ProcedureName = scheduling.Procedure.ServiceName,
            };

            return Result<SchedulingViewModel>.Success(result);
        }

    }
}
