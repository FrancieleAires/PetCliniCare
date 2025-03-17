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
    public class GetAnimalByIdQuery : IRequest<Result<AnimalViewModel>>
    {
        public int AnimalId { get; set; }

        public GetAnimalByIdQuery(int animalId)
        {
            AnimalId = animalId;
        }
    }
}
