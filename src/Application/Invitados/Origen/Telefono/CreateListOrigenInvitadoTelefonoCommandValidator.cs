using Application.Invitados.Origen.Telefono;
using FluentValidation;

namespace Application.Invitados.Origen
{
    public class CreateListOrigenInvitadoTelefonoCommandValidator
        : AbstractValidator<CreateListOrigenInvitadoTelefonoCommand>
    {
        public CreateListOrigenInvitadoTelefonoCommandValidator()
        {
            RuleFor(x => x.Telefonos)
                .NotNull().WithMessage("La lista de teléfonos no puede ser nula.")
                .NotEmpty().WithMessage("La lista de teléfonos no puede estar vacía.");

            RuleForEach(x => x.Telefonos)
                .SetValidator(new CreateOrigenInvitadoTelefonoDtoValidator());
        }
    }
}
