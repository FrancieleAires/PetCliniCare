using CliniCare.Application.Helpers;
using CliniCare.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CliniCare.Application.Commands.Procedures
{
    public class InsertProcedureCommand : IRequest<Result<Unit>>
    {

        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public Procedure ToEntity()
        {
            return new Procedure
            {
                ServiceName = ServiceName,
                Price = Price,
                Description = Description
            };
        }
    }
}
