using CliniCare.Application.Helpers;
using CliniCare.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Animals.UpdateAnimal
{
    public class UpdateAnimalCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public UpdateAnimalCommand(string species, string name, string breed, DateTime dateOfBirth, string gender)
        {
            Species = species;
            Name = name;
            Breed = breed;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

    }
}
