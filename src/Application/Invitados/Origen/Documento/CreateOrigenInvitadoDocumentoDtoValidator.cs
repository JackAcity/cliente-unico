using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invitados.Origen.Documento
{
    public class CreateOrigenInvitadoDocumentoDtoValidator
        : AbstractValidator<CreateOrigenInvitadoDocumentoCoreCommand>
    {
        public CreateOrigenInvitadoDocumentoDtoValidator()
        {
            RuleFor(x => x.InvitadoDocumentoId)
                .NotEqual(Guid.Empty).WithMessage("InvitadoDocumentoId inválido.");
        }
    }
}
