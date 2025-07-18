using FluentValidation;

namespace Application.Invitados.Origen.Correo
{
    public class CreateOrigenInvitadoCorreoDtoValidator
        : AbstractValidator<CreateOrigenInvitadoCorreoCoreCommand>
    {
        public CreateOrigenInvitadoCorreoDtoValidator()
        {
            RuleFor(x => x.InvitadoCorreoId)
                .NotEqual(Guid.Empty).WithMessage("InvitadoCorreoId inválido.");

            RuleFor(x => x.CodigoValidacion)
                .NotEmpty().WithMessage("CodigoValidacion es obligatorio.")
                .MaximumLength(50);
        }
    }
}
