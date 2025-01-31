using CliniCare.Application.InputModels.VeterinaryProcedure;
using CliniCare.Domain.Models;
using FluentValidation;

public class VeterinaryProcedureValidator : AbstractValidator<CreateVetProcedureInputModel>
{
    public VeterinaryProcedureValidator()
    {
        RuleFor(vp => vp.ServiceName)
            .NotEmpty()
            .WithMessage("Nome do serviço é obrigatório.")
            .MaximumLength(100)
            .WithMessage("Nome do serviço não pode exceder 100 caracteres.");

        RuleFor(vp => vp.Price)
            .GreaterThan(0)
            .WithMessage("Preço deve ser maior que zero.");

        RuleFor(vp => vp.Description)
            .MaximumLength(500)
            .WithMessage("Descrição não pode exceder 500 caracteres.");
    }
}
