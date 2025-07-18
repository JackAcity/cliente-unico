using FluentValidation;

namespace Application.Invitados.Origen.Telefono
{
    public class CreateOrigenInvitadoTelefonoDtoValidator
        : AbstractValidator<CreateOrigenInvitadoTelefonoCommand>
    {
        public CreateOrigenInvitadoTelefonoDtoValidator()
        {
            RuleFor(x => x.InvitadoTelefonoId)
                .NotEqual(Guid.Empty).WithMessage("InvitadoTelefonoId inválido.");

            RuleFor(x => x.CodigoValidacion)
                .NotEmpty().WithMessage("CodigoValidacion es obligatorio.")
                .MaximumLength(50);
        }
    }
}
