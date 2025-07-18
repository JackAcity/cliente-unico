using Application.Invitados.InvitadoOrchestrator.Create;
using Application.Invitados.InvitadoPincipal.Create;
using FluentValidation;

namespace Application.Invitados.DatosPincipales.Create
{
    public class CreateInvitadoPrincipalCommandValidator : AbstractValidator<CreateDatosPrincipalesDto>
    {
        public CreateInvitadoPrincipalCommandValidator()
        {
            RuleFor(x => x.TipoDocumentoCodigo)
                .NotEmpty().WithMessage("TipoDocumentoCodigo is required.");

            RuleFor(x => x.DocumentoIdentidad)
                .NotEmpty().WithMessage("DocumentoIdentidad is required.")
                .MaximumLength(20).WithMessage("DocumentoIdentidad must not exceed 20 characters.");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("Correo is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("Numero is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.Direccion)
                .MaximumLength(200).WithMessage("Direccion must not exceed 200 characters.");

            RuleFor(x => x.Referencia)
                .MaximumLength(200).WithMessage("Referencia must not exceed 200 characters.");
        }
    }
}
