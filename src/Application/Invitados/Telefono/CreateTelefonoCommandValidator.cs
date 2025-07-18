using Application.Invitados.InvitadoOrchestrator.Create;
using FluentValidation;

namespace Application.Invitados.Telefono
{
    public class CreateTelefonoCommandValidator : AbstractValidator<CreateTelefonoDto>
    {
        public CreateTelefonoCommandValidator()
        {
            RuleFor(x => x.TipoContactoCodigo)
                .NotEmpty().WithMessage("TipoContactoCodigo is required.");

            RuleFor(x => x.PrefijoPais)
                .NotEmpty().WithMessage("PrefijoPais is required.");

            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("Numero is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");
        }
    }
}
