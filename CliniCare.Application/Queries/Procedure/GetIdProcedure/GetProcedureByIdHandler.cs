using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Procedure.GetIdProcedure
{
    public class GetProcedureByIdHandler : IRequestHandler<GetProcedureByIdQuery, Result<ProcedureViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProcedureByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProcedureViewModel>> Handle(GetProcedureByIdQuery query, CancellationToken cancellationToken)
        {
            var procedure = await _unitOfWork.VeterinaryProcedures.GetByIdAsync(query.Id);
            if (procedure == null) return Result<ProcedureViewModel>.Failure("Não foi encontrado nenhum procedimento.");

            var procedureViewModel = new ProcedureViewModel
            {
                Id = procedure.Id,
                ServiceName = procedure.ServiceName,
                Price = procedure.Price,
                Description = procedure.Description,

            };

            return Result<ProcedureViewModel>.Success(procedureViewModel);
        }
    }
}
