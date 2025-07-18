using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invitados.Origen.Invitados
{
    public class CreateOrigenInvitadoCoreCommandValidator
        : AbstractValidator<CreateOrigenInvitadoCoreCommand>
    {
        public CreateOrigenInvitadoCoreCommandValidator()
        {
            RuleFor(x => x.SistemaCodigo)
                .NotEmpty().WithMessage("SistemaCodigo es obligatorio.")
                .MaximumLength(20);

            RuleFor(x => x.OrigenId)
                .NotEmpty().WithMessage("OrigenId es obligatorio.")
                .MaximumLength(50);

            RuleFor(x => x.FechaRegistroOrigen)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("FechaRegistroOrigen no puede ser mayor que la fecha actual.");

            RuleFor(x => x.InvitadoId)
                .NotEqual(Guid.Empty).WithMessage("InvitadoId inválido.");

            When(x => x.EstadoId.HasValue, () =>
            {
                RuleFor(x => x.EstadoId.Value)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("EstadoId debe ser mayor o igual a cero.");
            });
        }
    }
}
