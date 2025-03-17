using CliniCare.Application.Helpers;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Animal
{
    public class GetAnimalByIdHandler : IRequestHandler<GetAnimalByIdQuery, Result<AnimalViewModel>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUser _user;
        public GetAnimalByIdHandler(IUnitOfWork unitOfWork, IApplicationUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<Result<AnimalViewModel>> Handle(GetAnimalByIdQuery query, CancellationToken cancellationToken)
        {
            var userId = _user.Id;

            var client = await _unitOfWork.Clients.GetClientByUserIdAsync(userId);
            if (client == null)
            {
                return Result<AnimalViewModel>.Failure("Não foi encontrado o usuário autenticado.");
            }
            var animal = await _unitOfWork.Animals.GetAnimalByIdAsync(query.AnimalId);
            if (animal == null) return Result<AnimalViewModel>.Failure("Não foi possível consultar esse animal.");

            if (animal.ClientId != client.Id)
            {
                return Result<AnimalViewModel>.Failure("Este animal não pertence ao cliente autenticado.");
            }


            var animalViewModel = new AnimalViewModel 
            {
                Id = animal.Id,
                Breed = animal.Breed,
                Species = animal.Species,
                Name = animal.Name,
                Gender = animal.Gender,
                DateOfBirth = animal.DateOfBirth, 
                
            };

            return Result<AnimalViewModel>.Success(animalViewModel);
        }
    }
}
