using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.VeterinaryCare
{
    public class GetVetCareByIdQuery : IRequest<Result<VetCareViewModel>>
    {
        public int Id { get; set; }

        public GetVetCareByIdQuery(int id)
        {
            Id = id;
        }
    }
}
