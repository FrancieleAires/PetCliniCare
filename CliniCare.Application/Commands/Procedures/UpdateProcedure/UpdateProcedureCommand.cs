using CliniCare.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CliniCare.Application.Commands.Procedures.UpdateProcedure
{
    public class UpdateProcedureCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public UpdateProcedureCommand(string serviceName, double price, string description)
        {
            ServiceName = serviceName;
            Price = price;
            Description = description;
        }
    }
}
