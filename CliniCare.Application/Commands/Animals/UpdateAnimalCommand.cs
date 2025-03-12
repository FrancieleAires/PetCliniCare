using CliniCare.Application.Helpers;
using CliniCare.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Animals
{
    public class UpdateAnimalCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public UpdateAnimalCommand(int id, string species, string name, string breed, DateTime dateOfBirth, string gender)
        {
            Id = id;
            Species = species;
            Name = name;
            Breed = breed;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        public Animal ToEntity()
        {
            return new Animal
            {
                Id = Id,
                Species = Species,
                Name = Name,
                Gender = Gender,
                DateOfBirth = DateOfBirth,
                Breed = Breed,

            };
        }
    }
}
