using CliniCare.Application.Commands.Animals;
using CliniCare.Domain.Models;
using FluentValidation;
using System;

namespace CliniCare.Application.Validators
{
    public class UpdateAnimalValidator : AbstractValidator<UpdateAnimalCommand>
    {
        public UpdateAnimalValidator()
        {
            RuleFor(a => a.Species)
                .MaximumLength(50)
                .WithMessage("Espécie não pode exceder 50 caracteres.");

            RuleFor(a => a.Name)
                .MaximumLength(50)
                .WithMessage("Nome não pode exceder 50 caracteres.");

            RuleFor(a => a.Breed)
                .MaximumLength(50)
                .WithMessage("Raça não pode exceder 50 caracteres.");

            RuleFor(a => a.Gender)
                .Must(g => g == "Macho" || g == "Fêmea")
                .WithMessage("Sexo deve ser 'Macho' ou 'Fêmea'.");

            RuleFor(a => a.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Data de nascimento deve ser até hoje.");
        }
    }
}
