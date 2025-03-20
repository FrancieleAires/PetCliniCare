using CliniCare.Application.Helpers;
using CliniCare.Application.Queries.Animal;
using CliniCare.Application.ViewModels;
using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Procedure
{
    public class GetAllProcedureHandler : IRequestHandler<GetAllProcedureQuery, Result<List<ProcedureViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProcedureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<ProcedureViewModel>>> Handle(GetAllProcedureQuery query, CancellationToken cancellationToken)
        {
            var procedures = await _unitOfWork.VeterinaryProcedures.GetAllVeterinaryProcedureAsync();
            if (procedures == null) return Result<List<ProcedureViewModel>>.Failure("Não possui nenhum procedimento. ");
            var procedureViewModel = procedures.Select(a => new ProcedureViewModel
            {
                Id = a.Id,
                Price = a.Price,
                Description = a.Description,
                ServiceName = a.ServiceName,

            }).ToList();

            return Result<List<ProcedureViewModel>>.Success(procedureViewModel);
        }
    }
}
