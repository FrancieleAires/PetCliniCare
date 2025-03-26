using CliniCare.Application.Helpers;
using CliniCare.Application.Queries.Schedulings;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.VeterinaryCare
{
    public class GetVetCareByIdHandler : IRequestHandler<GetVetCareByIdQuery, Result<VetCareViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetVetCareByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<VetCareViewModel>> Handle(GetVetCareByIdQuery query, CancellationToken cancellationToken)
        {
            var vetCare = await _unitOfWork.VeterinaryCares.GetCareByIdAsync(query.Id);
            if (vetCare == null) return Result<VetCareViewModel>.Failure("Não foi encontrado esse tratamento veterinário.");

            var result = new VetCareViewModel
            {
                Id = vetCare.Id,
                Treatment = vetCare.Treatment,
                CareDate = vetCare.CareDate,
                ProblemDescription = vetCare.ProblemDescription,
                VeterinarianName = vetCare.Veterinarian.Name,
                AnimalName = vetCare.Animal.Name,
                SchedulingId = vetCare.Scheduling.Id,
            };

            return Result<VetCareViewModel>.Success(result);
        }
    }
}
