using CliniCare.Application.Helpers;
using CliniCare.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Procedures.InsertProcedure
{
    public class InsertProcedureHandler : IRequestHandler<InsertProcedureCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertProcedureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<Unit>> Handle(InsertProcedureCommand request, CancellationToken cancellationToken)
        {
            var procedure = request.ToEntity();
            if (procedure == null) return Result<Unit>.Failure("Não foi encontrado nenhum dado de procedimento, tente novamente mais tarde.");

            await _unitOfWork.VeterinaryProcedures.AddAsync(procedure);
            var result = await _unitOfWork.CommitAsync();

            if (!result)
            {
                return Result<Unit>.Failure("Não foi possível criar um procedimento, por favor, tente novamente.");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
