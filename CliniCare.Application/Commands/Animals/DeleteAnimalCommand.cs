using CliniCare.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Animals
{
    public class DeleteAnimalCommand : IRequest<Result<Unit>>
    {
        public int AnimalId { get; set; }

        public DeleteAnimalCommand(int animalId)
        {
            AnimalId = animalId;
        }
    }
}
