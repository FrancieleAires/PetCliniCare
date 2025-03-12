using CliniCare.Application.Helpers;
using MediatR;
using System;
using CliniCare.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Animals
{
    public class InsertAnimalCommand : IRequest<Result<Unit>>
    {
        public string Species { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }


        public Animal ToEntity(int clientId)
        {
            return new Animal
            {
                Species = Species,
                Name = Name,
                Gender = Gender,
                DateOfBirth = DateOfBirth,
                Breed = Breed,
                ClientId = clientId
            };
        }
    }
}
