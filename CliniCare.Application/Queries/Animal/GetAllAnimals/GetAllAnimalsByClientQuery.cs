using CliniCare.Application.Helpers;
using CliniCare.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Queries.Animal.GetAllAnimals
{
    public class GetAllAnimalsByClientQuery : IRequest<Result<List<AnimalViewModel>>>
    {
        public GetAllAnimalsByClientQuery()
        {

        }
    }
}
