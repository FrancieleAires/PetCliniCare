using CliniCare.Application.Commands.Schedulings.InsertSchedulingClient;
using CliniCare.Domain.Models;
using CliniCare.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Commands.Schedulings.InsertScheduling
{
    public class SchedulingValidator : AbstractValidator<InsertSchedulingClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IVeterinaryProcedureRepository _veterinaryProcedureRepository;
        private readonly IVeterinarianRepository _veterinarianRepository;

        public SchedulingValidator(
            IClientRepository clientRepository,
            IAnimalRepository animalRepository,
            IVeterinaryProcedureRepository veterinaryProcedureRepository,
            IVeterinarianRepository veterinarianRepository)
        {
            _clientRepository = clientRepository;
            _animalRepository = animalRepository;
            _veterinaryProcedureRepository = veterinaryProcedureRepository;
            _veterinarianRepository = veterinarianRepository;


            RuleFor(s => s.VeterinarianId)
                 .NotEmpty().WithMessage("O ID do veterinário é obrigatório.")
                 .GreaterThan(0).WithMessage("O ID do veterinário deve ser maior que zero.");

            RuleFor(s => s.AnimalId)
                .NotEmpty().WithMessage("O ID do animal é obrigatório.")
                .GreaterThan(0).WithMessage("O ID do animal deve ser maior que zero.");

            RuleFor(s => s.ProcedureId)
                .NotEmpty().WithMessage("O ID do serviço é obrigatório.")
                .GreaterThan(0).WithMessage("O ID do serviço deve ser maior que zero.");

            RuleFor(s => s.SchedulingDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("A data do agendamento não pode ser no passado.");

            RuleFor(s => s.SchedulingTime)
                .NotEqual(TimeSpan.Zero)
                .WithMessage("O horário do agendamento é obrigatório.");
        }
    }
}
