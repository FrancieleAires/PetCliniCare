using CliniCare.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Animals.InsertAnimal
{
    public class AnimalValidator : AbstractValidator<InsertAnimalCommand>
    {
        public AnimalValidator()
        {
            RuleFor(a => a.Species)
           .NotEmpty()
           .WithMessage("Espécie do animal é obrigatória.")
           .MaximumLength(50)
           .WithMessage("Espécie não pode exceder 50 caracteres.");

            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Nome do animal é obrigatório.")
                .MaximumLength(50)
                .WithMessage("Nome não pode exceder 50 caracteres.");

            RuleFor(a => a.Breed)
                .NotEmpty()
                .WithMessage("Raça do animal é obrigatória.")
                .MaximumLength(50)
                .WithMessage("Raça não pode exceder 50 caracteres.");

            RuleFor(a => a.Gender)
                .NotEmpty()
                .WithMessage("Sexo do animal é obrigatório.")
                .Must(g => g == "Macho" || g == "Fêmea")
                .WithMessage("Sexo deve ser 'Macho' ou 'Fêmea'.");

            RuleFor(a => a.DateOfBirth)
                .NotEmpty()
                .WithMessage("Data de nascimento é obrigatória.")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Data de nascimento deve ser até hoje.");


        }

    }
}
