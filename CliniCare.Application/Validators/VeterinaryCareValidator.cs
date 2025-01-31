using CliniCare.Application.InputModels.VeterinaryCare;
using CliniCare.Domain.Models;
using CliniCare.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Validators
{
    public class VeterinaryCareValidator : AbstractValidator<CreateVetCareInputModel>
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IVeterinarianRepository _veterinarianRepository;

        public VeterinaryCareValidator(IAnimalRepository animalRepository,
            IVeterinarianRepository veterinarianRepository)
        {
            _animalRepository = animalRepository;
            _veterinarianRepository = veterinarianRepository;

            RuleFor(vc => vc.CareDate)
            .NotEmpty().WithMessage("A data do atendimento é obrigatória.")
            .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("A data do atendimento não pode ser no passado.");

            RuleFor(vc => vc.ProblemDescription)
                .NotEmpty().WithMessage("A descrição do problema é obrigatória.")
                .MinimumLength(10).WithMessage("A descrição do problema deve ter pelo menos 10 caracteres.")
                .MaximumLength(500).WithMessage("A descrição do problema pode ter no máximo 500 caracteres.");

            RuleFor(vc => vc.Treatment)
                .NotEmpty().WithMessage("O tratamento é obrigatório.")
                .MinimumLength(5).WithMessage("O tratamento deve ter pelo menos 5 caracteres.")
                .MaximumLength(500).WithMessage("O tratamento pode ter no máximo 500 caracteres.");

            RuleFor(s => s.VeterinarianId)
                .NotEmpty().WithMessage("O ID do veterinário é obrigatório.")
                .MustAsync(async (id, ct) => await _veterinarianRepository.ExistsAsync(id))
                .WithMessage("Veterinário inválido.");


            RuleFor(s => s.AnimalId)
                .NotEmpty().WithMessage("O ID do animal é obrigatório.")
                .MustAsync(async (id, ct) => await _animalRepository.ExistsAsync(id))
                .WithMessage("Animal inválido.");
        }
    }
}
