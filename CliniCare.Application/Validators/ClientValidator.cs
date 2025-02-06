using CliniCare.Application.InputModels.Client;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Validators
{
    public class ClientValidator : AbstractValidator<CreateClientInputModel>
    {
        public ClientValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome é obrigatório")
                .MaximumLength(50)
                .WithMessage("O nome não pode ter mais de 50 caracteres.");
            RuleFor(c => c.Phone)
                .NotEmpty()
                .WithMessage("Telefone não pode ser vazio e é obrigatório!");

            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Must(ValidarCPF).WithMessage("O CPF informado é inválido.");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("O endereço é obrigatório.")
                .MaximumLength(100).WithMessage("O endereço deve ter no máximo 100 caracteres.");
        }

            private bool ValidarCPF(string cpf)
            {
               
                if (string.IsNullOrWhiteSpace(cpf)) return false;

                cpf = cpf.Replace(".", "").Replace("-", "");

                if (cpf.Length != 11 || !long.TryParse(cpf, out _)) return false;

                var iguais = true;
                for (var i = 1; i < cpf.Length && iguais; i++)
                    if (cpf[i] != cpf[0]) iguais = false;

                if (iguais || cpf == "12345678909") return false;

                var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                var tempCpf = cpf.Substring(0, 9);
                var soma = 0;

                for (var i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

                var resto = soma % 11;
                var digito = resto < 2 ? 0 : 11 - resto;

                tempCpf += digito;
                soma = 0;

                for (var i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                digito = resto < 2 ? 0 : 11 - resto;

                return cpf.EndsWith(digito.ToString());
            }

        }
    }

