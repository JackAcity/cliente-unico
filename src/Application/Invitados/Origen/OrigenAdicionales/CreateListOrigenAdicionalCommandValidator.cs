using FluentValidation;

namespace Application.Invitados.Origen.OrigenAdicionales
{
    public class CreateListOrigenAdicionalCommandValidator
        : AbstractValidator<CreateListOrigenAdicionalCommand>
    {
        public CreateListOrigenAdicionalCommandValidator()
        {
            RuleFor(x => x.Adicionales)
                .NotNull().WithMessage("La lista de datos adicionales no puede ser nula.")
                .NotEmpty().WithMessage("La lista de datos adicionales no puede estar vacía.");

            RuleForEach(x => x.Adicionales)
                .SetValidator(new CreateOrigenAdicionalDtoValidator());
        }
    }
}
