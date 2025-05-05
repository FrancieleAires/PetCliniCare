using CliniCare.Application.Commands.Procedures.UpdateProcedure;
using FluentValidation;

public class UpdateProcedureValidator : AbstractValidator<UpdateProcedureCommand>
{
    public UpdateProcedureValidator()
    {

        RuleFor(vp => vp.ServiceName)
            .MaximumLength(100)
            .WithMessage("Nome do serviço não pode exceder 100 caracteres.")
            .When(vp => !string.IsNullOrEmpty(vp.ServiceName));

        RuleFor(vp => vp.Price)
            .GreaterThan(0)
            .WithMessage("Preço deve ser maior que zero.")
            .When(vp => vp.Price > 0);  

        RuleFor(vp => vp.Description)
            .MaximumLength(500)
            .WithMessage("Descrição não pode exceder 500 caracteres.")
            .When(vp => !string.IsNullOrEmpty(vp.Description)); 
    }
}
