using CliniCare.Application.InputModels.Veterinarian;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Validators
{
    public class VeterinarianValidator : AbstractValidator<CreateVeterinarianInputModel>
    {
        public VeterinarianValidator()
        {
            RuleFor(v => v.CRMV)
            .NotEmpty().WithMessage("O CRMV é obrigatório.")
            .Matches(@"^\d{4,6}-[A-Z]{2}$").WithMessage("O CRMV deve ter entre 4 e 6 números seguidos pela sigla do estado (ex.: 12345-SP).");
            RuleFor(v => v.Specialty)
                .NotNull()
                .WithMessage("A especialidade não pode ser nula");
        }
    }
}
