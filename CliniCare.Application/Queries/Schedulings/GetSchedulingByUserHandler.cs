using CliniCare.Application.Helpers;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Schedulings
{
    public class GetSchedulingByUserHandler : IRequestHandler<GetSchedulingsByUserQuery, Result<List<SchedulingViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUser _user;

        public GetSchedulingByUserHandler(IUnitOfWork unitOfWork, IApplicationUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<Result<List<SchedulingViewModel>>> Handle(GetSchedulingsByUserQuery query, CancellationToken cancellationToken)
        {
            var userId = query.UserId;
            var client = await _unitOfWork.Clients.GetClientByUserIdAsync(userId);
            if (client == null)
            {
                return Result<List<SchedulingViewModel>>.Failure("Não foi encontrado nenhum agendamento para esse usuário.");
            }

            var schedulings = await _unitOfWork.Schedulings.GetAllSchedulingsByClientIdAsync(client.Id);
            if (!schedulings.Any())
            {
                return Result<List<SchedulingViewModel>>.Failure("Não há agendamentos para esse usuário.", ErrorType.Validation);
            }

            var result = schedulings.Select(s => new SchedulingViewModel
            {
                Id = s.Id,
                SchedulingDate = s.SchedulingDate,
                SchedulingTime = s.SchedulingTime,
                SchedulingsStatus = s.SchedulingsStatus,
                Observation = s.Observation,
                AnimalName = s.Animal.Name,
                VeterinarianName = s.Veterinarian.Name,
                ProcedureName = s.Procedure.ServiceName,
            }).ToList();

            return Result<List<SchedulingViewModel>>.Success(result);
        }
    }
}
