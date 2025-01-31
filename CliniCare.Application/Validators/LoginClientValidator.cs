using CliniCare.Application.InputModels.Client;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Validators
{
    public class LoginClientValidator : AbstractValidator<LoginClientInputModel>
    {
        public LoginClientValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.");
        }
    }
}
