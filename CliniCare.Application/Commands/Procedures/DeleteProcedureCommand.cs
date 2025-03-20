using CliniCare.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Procedures
{
    public class DeleteProcedureCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }

        public DeleteProcedureCommand(int id)
        {
            Id = id;
        }
    }
}
