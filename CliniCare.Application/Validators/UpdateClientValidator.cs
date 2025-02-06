using CliniCare.Application.InputModels.Client;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Validators
{
    public class UpdateClientValidator : AbstractValidator<UpdateClientInputModel>
    {
        public UpdateClientValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome não pode ser vazio.")
                .MaximumLength(50)
                .WithMessage("O nome não pode ter mais de 50 caracteres.");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("O endereço é obrigatório.")
                .MaximumLength(100).WithMessage("O endereço deve ter no máximo 100 caracteres.");
        }
    }
}
