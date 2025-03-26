using CliniCare.Application.Helpers;
using CliniCare.Application.Queries.Schedulings;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.VeterinaryCare
{
    public class GetAllVetCaresHandler : IRequestHandler<GetAllVetCaresQuery, Result<List<VetCareViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllVetCaresHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<VetCareViewModel>>> Handle(GetAllVetCaresQuery query, CancellationToken cancellationToken)
        {
            var vetCare = await _unitOfWork.VeterinaryCares.GetAllVeterinaryCareAsync();
            if (vetCare == null || !vetCare.Any())
            {
                return Result<List<VetCareViewModel>>.Failure("Nenhum tratamento veterinário encontrado.");
            }

            var vetCareViewModel = vetCare.Select(v => new VetCareViewModel
            {
                Id = v.Id,
                CareDate = v.CareDate,
                Treatment = v.Treatment,
                ProblemDescription = v.ProblemDescription,
                SchedulingId = v.SchedulingId,
                AnimalName = v.Animal.Name,
                VeterinarianName = v.Veterinarian.Name
            }).ToList();

            return Result<List<VetCareViewModel>>.Success(vetCareViewModel);

        }
    }
}
