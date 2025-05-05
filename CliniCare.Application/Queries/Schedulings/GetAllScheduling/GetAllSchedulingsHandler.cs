using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Schedulings.GetAllScheduling
{
    public class GetAllSchedulingsHandler : IRequestHandler<GetAllSchedulingsQuery, Result<List<SchedulingViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSchedulingsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<SchedulingViewModel>>> Handle(GetAllSchedulingsQuery query, CancellationToken cancellationToken)
        {
            var schedulings = await _unitOfWork.Schedulings.GetAllSchedulingAsync();

            if (schedulings == null || !schedulings.Any())
            {
                return Result<List<SchedulingViewModel>>.Failure("Nenhum agendamento encontrado.");
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
