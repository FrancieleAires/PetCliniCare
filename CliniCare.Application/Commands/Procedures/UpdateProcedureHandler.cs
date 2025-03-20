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
    public class UpdateProcedureHandler : IRequestHandler<UpdateProcedureCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWorrk;

        public UpdateProcedureHandler(IUnitOfWork unitOfWorrk)
        {
            _unitOfWorrk = unitOfWorrk;
        }

        public async Task<Result<Unit>> Handle(UpdateProcedureCommand request, CancellationToken cancellationToken)
        {
            var procedure = await _unitOfWorrk.VeterinaryProcedures.GetByIdAsync(request.Id);
            if (procedure == null) return Result<Unit>.Failure("Não foi encontrado nenhum procedimento com esse ID, tente novamente.");

            procedure.Price = request.Price;
            procedure.ServiceName = request.ServiceName;
            procedure.Description = request.Description;

            await _unitOfWorrk.VeterinaryProcedures.UpdateAsync(procedure);
            var result = await _unitOfWorrk.CommitAsync();
            if (!result)
            {
                return Result<Unit>.Failure("Não foi possível fazer as alterações do procedimento, tente novamente.");
            }
            
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
