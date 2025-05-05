using CliniCare.Application.Commands.Animals;
using CliniCare.Application.Helpers;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.VetCares.DeleteVetCare
{
    public class DeleteVetCareHandler : IRequestHandler<DeleteVetCareCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVetCareHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteVetCareCommand request, CancellationToken cancellationToken)
        {
            var vetCare = await _unitOfWork.VeterinaryCares.GetCareByIdAsync(request.Id);
            if (vetCare == null) return Result<Unit>.Failure("Não foi encontrado nenhum tratamento veterinário.");

            await _unitOfWork.VeterinaryCares.DeleteVeterinaryCareAsync(vetCare);
            var result = await _unitOfWork.CommitAsync();
            if (!result) return Result<Unit>.Failure("Erro ao deletar o tratamento veterinário. Por favor, tente novamente ou entre em contato com o suporte.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
