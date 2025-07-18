using Application.Invitados.Origen.Correo;
using FluentValidation;

namespace Application.Invitados.Origen
{
    public class CreateListOrigenInvitadoCorreoCommandValidator
        : AbstractValidator<CreateListOrigenInvitadoCorreoCommand>
    {
        public CreateListOrigenInvitadoCorreoCommandValidator()
        {
            RuleFor(x => x.Correos)
                .NotNull().WithMessage("La lista de correos no puede ser nula.")
                .NotEmpty().WithMessage("La lista de correos no puede estar vacía.");

            RuleForEach(x => x.Correos)
                .SetValidator(new CreateOrigenInvitadoCorreoDtoValidator());
        }
    }
}
