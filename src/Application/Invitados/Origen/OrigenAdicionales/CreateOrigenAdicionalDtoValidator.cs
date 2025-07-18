using FluentValidation;

namespace Application.Invitados.Origen.OrigenAdicionales
{
    public class CreateOrigenAdicionalDtoValidator
        : AbstractValidator<CreateOrigenAdicionalCommand>
    {
        public CreateOrigenAdicionalDtoValidator()
        {
            RuleFor(x => x.CampoPersonalizadoId)
                .GreaterThan(0).WithMessage("CampoPersonalizadoId debe ser mayor que cero.");

            RuleFor(x => x.Valor)
                .NotEmpty().WithMessage("Valor es obligatorio.")
                .MaximumLength(200);
        }
    }
}
