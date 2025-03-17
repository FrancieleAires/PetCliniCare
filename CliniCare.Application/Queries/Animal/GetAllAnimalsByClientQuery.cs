using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Animal
{
    public class GetAllAnimalsByClientQuery : IRequest<Result<List<AnimalViewModel>>>
    {
        public int ClientId { get; set; }

        public GetAllAnimalsByClientQuery(int clientId)
        {
            ClientId = clientId;
        }
    }
}
