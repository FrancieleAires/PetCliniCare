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

namespace CliniCare.Application.Queries.DashboardClient
{
    public class DashboardClientHandler : IRequestHandler<DashboardClientQuery, Result<DashboardViewModel>>
    {
        private readonly IApplicationUser _user;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardClientHandler(IApplicationUser user, IUnitOfWork unitOfWork)
        {
            _user = user;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DashboardViewModel>> Handle(DashboardClientQuery query, CancellationToken cancellationToken)
        {
            var userId = _user.Id;

            var client = await _unitOfWork.Clients.GetCurrentClientAsync(userId);
            if(client == null) return Result<DashboardViewModel>.Failure("Cliente não encontrado.");

            var animals = await _unitOfWork.Animals.GetAllAnimalsByClientAsync(client.Id);
            var schedulings = await _unitOfWork.Schedulings.GetAllSchedulingsByClientIdAsync(client.Id);

            var animalsCount = animals.Count();
            var nextSchedulingsCount = schedulings.Count(s => s.SchedulingsStatus == Domain.Enums.SchedulingsStatus.Confirmado);
            var finishedSchedulingsCount = schedulings.Count(s => s.SchedulingsStatus == Domain.Enums.SchedulingsStatus.Finalizado);

            var dashBoardViewModel = new DashboardViewModel
            {
                Info = new DashInfos
                {
                    AmountPetsCreated = animalsCount,
                    AmountNextSchedulings = nextSchedulingsCount,
                    AmountFinishedSchedulings = finishedSchedulingsCount
                },
                NextSchedulings = schedulings
                    .Where(s => s.SchedulingsStatus == Domain.Enums.SchedulingsStatus.Confirmado)
                    .Select(s => new SchedulingViewModel
                    {
                        Id = s.Id,
                        ProcedureName = s.Procedure.ServiceName,
                        SchedulingDate = s.SchedulingDate,
                        SchedulingTime = s.SchedulingTime,
                        AnimalName = s.Animal.Name,
                        VeterinarianName = s.Veterinarian.Name,
                        SchedulingsStatus = s.SchedulingsStatus,
                    })
                    .ToList(),
                Pets = animals.Select(a => new AnimalViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Breed = a.Breed,
                    Species = a.Species,
                    DateOfBirth = a.DateOfBirth,
                }).ToList(),
            };
            return Result<DashboardViewModel>.Success(dashBoardViewModel);
        }
    }
}
