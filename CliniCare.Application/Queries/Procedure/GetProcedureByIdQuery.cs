using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Procedure
{
    public class GetProcedureByIdQuery : IRequest<Result<ProcedureViewModel>>
    {
        public int Id { get; set; }

        public GetProcedureByIdQuery(int id)
        {
            Id = id;
        }
    }
}
