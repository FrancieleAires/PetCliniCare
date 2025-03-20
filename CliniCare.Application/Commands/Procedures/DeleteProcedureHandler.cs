using CliniCare.Application.Helpers;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Procedures
{
    public class DeleteProcedureHandler :IRequestHandler<DeleteProcedureCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProcedureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteProcedureCommand request, CancellationToken cancellationToken)
        {
            var procedure = await _unitOfWork.VeterinaryProcedures.GetByIdAsync(request.Id);
            if (procedure == null) return Result<Unit>.Failure("Ñão foi encontrado nenhum procedimento.");

            await _unitOfWork.VeterinaryProcedures.DeleteVeterinaryProcedureAsync(procedure);
            var result = await _unitOfWork.CommitAsync();
            if(!result)
            {
                return Result<Unit>.Failure("Não foi possível deletar esse procedimento, por favor, tente novamente.");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
