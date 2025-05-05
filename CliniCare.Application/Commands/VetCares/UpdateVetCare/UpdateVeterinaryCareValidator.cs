using CliniCare.Application.Commands.VetCares;
using CliniCare.Domain.Models;
using CliniCare.Domain.Repositories;
using FluentValidation;
using System;

namespace CliniCare.Application.Commands.VetCares.UpdateVetCare
{
    public class UpdateVeterinaryCareValidator : AbstractValidator<UpdateVetCareCommand>
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IVeterinarianRepository _veterinarianRepository;

        public UpdateVeterinaryCareValidator(IAnimalRepository animalRepository,
            IVeterinarianRepository veterinarianRepository)
        {
            _animalRepository = animalRepository;
            _veterinarianRepository = veterinarianRepository;

            RuleFor(vc => vc.CareDate)
                .NotEmpty().WithMessage("A data do atendimento é obrigatória.")
                .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("A data do atendimento não pode ser no passado.");

            RuleFor(vc => vc.ProblemDescription)
                .MaximumLength(500).WithMessage("A descrição do problema pode ter no máximo 500 caracteres.")
                .When(vc => !string.IsNullOrEmpty(vc.ProblemDescription));

            RuleFor(vc => vc.Treatment)
                .MaximumLength(500).WithMessage("O tratamento pode ter no máximo 500 caracteres.")
                .When(vc => !string.IsNullOrEmpty(vc.Treatment));

        }
    }
}
