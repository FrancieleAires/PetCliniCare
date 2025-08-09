using CliniCare.Application.Helpers;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.DashboardClinic
{
    public class DashboardClinicHandler : IRequestHandler<DashboardClinicQuery, Result<DashboardClinicViewModel>>
    {
        private readonly IApplicationUser _user;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardClinicHandler(IApplicationUser user, IUnitOfWork unitOfWork)
        {
            _user = user;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DashboardClinicViewModel>> Handle(DashboardClinicQuery query, CancellationToken cancellationToken)
        {
            var userId = _user.Id;

            var clients = await _unitOfWork.Clients.GetAllClientAsync();  
            var schedulings = await _unitOfWork.Schedulings.GetAllSchedulingAsync();

            var clientsCount = clients.Count();
            var finishedSchedulingsCount = schedulings.Count(s => s.SchedulingsStatus == Domain.Enums.SchedulingsStatus.Finalizado);
            var pendingSchedulingsCount = schedulings.Count(s => s.SchedulingsStatus == Domain.Enums.SchedulingsStatus.Pendente);
            var currentSchedulingsCount = schedulings.Count(s => s.SchedulingDate == DateTime.Today);
            var dashBoardViewModel = new DashboardClinicViewModel
            {
                infos = new DashboardClinicViewModel.DashClinicInfo
                {
                    AmountClientsCreated = clientsCount,
                    AmountFinishedScheduling = finishedSchedulingsCount,
                    AmountCurrentsShedulings = currentSchedulingsCount,
                    AmountPendingScheduling = pendingSchedulingsCount  
                },
                CurrentSchedulings = schedulings
                    .Where(s => s.SchedulingDate == DateTime.Today)
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
                    .ToList()
            };

            return Result<DashboardClinicViewModel>.Success(dashBoardViewModel);
        }
    }
}
