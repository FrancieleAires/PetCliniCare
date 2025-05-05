using CliniCare.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.VetCares.DeleteVetCare
{
    public class DeleteVetCareCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }

        public DeleteVetCareCommand(int id)
        {
            Id = id;
        }
    }
}
