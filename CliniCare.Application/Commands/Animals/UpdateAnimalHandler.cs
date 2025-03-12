using CliniCare.Application.Helpers;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Animals
{
    public class UpdateAnimalHandler : IRequestHandler<UpdateAnimalCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAnimalHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
        {
            var animal = await _unitOfWork.Animals.GetAnimalByIdAsync(request.Id);
            if (animal == null) return Result<Unit>.Failure("Nenhum animal encontrado com esse ID.");

            animal = request.ToEntity();

            await _unitOfWork.Animals.UpdateAnimalAsync(animal);
            var result = await _unitOfWork.CommitAsync();
            if(!result) return Result<Unit>.Failure("Erro ao atualizar os dados.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
