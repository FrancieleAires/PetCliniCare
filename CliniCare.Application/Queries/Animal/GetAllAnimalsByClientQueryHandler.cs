using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Animal
{
    public class GetAllAnimalsByClientQueryHandler : IRequestHandler<GetAllAnimalsByClientQuery, Result<List<AnimalViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAnimalsByClientQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<AnimalViewModel>>> Handle(GetAllAnimalsByClientQuery request, CancellationToken cancellationToken)
        {
            var animal = await _unitOfWork.Animals.GetAllAnimalAsync(request.ClientId);
            if(animal == null)
            {
                return Result<List<AnimalViewModel>>.Failure("Não foram encontrados animais.");
            }

            var animalViewModel = animal.Select(a => new AnimalViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Species = a.Species,
                Breed = a.Breed,
                DateOfBirth = a.DateOfBirth,
                Gender = a.Gender
            }).ToList();

            return Result<List<AnimalViewModel>>.Success(animalViewModel);


        }

    }
    }

