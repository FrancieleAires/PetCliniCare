using CliniCare.Application.Helpers;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.VetCares.InsertVetCare
{
    public class InsertVetCareHandler : IRequestHandler<InsertVetCareCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUser _user;
        public InsertVetCareHandler(IApplicationUser user, IUnitOfWork unitOfWork)
        {
            _user = user;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(InsertVetCareCommand request, CancellationToken cancellationToken)
        {
            var userId = _user.Id;
            var veterinarian = await _unitOfWork.Veterinarians.GetCurrentVeterinarianAsync(userId);
            if (veterinarian == null) return Result<Unit>.Failure("Não foi encontrado nenhum veterinário autenticado.");

            var vetCare = request.ToEntity(veterinarian.Id);
            await _unitOfWork.VeterinaryCares.AddVeterinaryCareAsync(vetCare);
            var result = await _unitOfWork.CommitAsync();

            if (!result) return Result<Unit>.Failure("Não foi possível criar um novo tratamento");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
