using Application.Invitados.InvitadoOrchestrator.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invitados.Correos.Create
{
    public class CreateCorreoCommandValidator : AbstractValidator<CreateCorreoDto>
    {
        public CreateCorreoCommandValidator()
        {
            RuleFor(x => x.TipoContactoCodigo)
                .NotEmpty().WithMessage("TipoContactoCodigo is required.");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("Correo is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Correo must not exceed 100 characters.");
        }
    }
}
