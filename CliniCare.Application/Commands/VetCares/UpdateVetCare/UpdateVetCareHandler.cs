using CliniCare.Application.Helpers;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.VetCares.UpdateVetCare
{
    public class UpdateVetCareHandler : IRequestHandler<UpdateVetCareCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVetCareHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateVetCareCommand request, CancellationToken cancellationToken)
        {
            var vetCare = await _unitOfWork.VeterinaryCares.GetCareByIdAsync(request.Id);
            if (vetCare == null) return Result<Unit>.Failure("Não foi encontrado nenhum tratamento veterinário.");

            vetCare.CareDate = request.CareDate;
            vetCare.ProblemDescription = request.ProblemDescription;
            vetCare.Treatment = request.Treatment;

            await _unitOfWork.VeterinaryCares.UpdateVeterinaryCareAsync(vetCare);
            var result = await _unitOfWork.CommitAsync();
            if (!result) return Result<Unit>.Failure("Erro ao atualizar os dados.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}

