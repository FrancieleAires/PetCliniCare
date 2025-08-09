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

namespace CliniCare.Application.Queries.Animal.GetAllAnimals
{
    public class GetAllAnimalsByClientHandler : IRequestHandler<GetAllAnimalsByClientQuery, Result<List<AnimalViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUser _user;

        public GetAllAnimalsByClientHandler(IUnitOfWork unitOfWork, IApplicationUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }
        public async Task<Result<List<AnimalViewModel>>> Handle(GetAllAnimalsByClientQuery query, CancellationToken cancellationToken)
        {
            var userId = _user.Id;

            var animals = await _unitOfWork.Animals.GetAllAnimalsByClientAsync(userId);
            if (animals == null || !animals.Any())
            {
                return Result<List<AnimalViewModel>>.Failure("Nenhum animal encontrado.");
            }
            var result = animals.Select(a => new AnimalViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Species = a.Species,
                Breed = a.Breed,
                DateOfBirth = a.DateOfBirth,
                Gender = a.Gender
            }).ToList();

            return Result<List<AnimalViewModel>>.Success(result);
        }
    }
}
