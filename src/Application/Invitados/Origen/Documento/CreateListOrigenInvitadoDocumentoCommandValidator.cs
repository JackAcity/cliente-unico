using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invitados.Origen.Documento
{
    public class CreateListOrigenInvitadoDocumentoCommandValidator
        : AbstractValidator<CreateListOrigenInvitadoDocumentoCommand>
    {
        public CreateListOrigenInvitadoDocumentoCommandValidator()
        {
            RuleFor(x => x.Documentos)
                .NotNull().WithMessage("La lista de documentos no puede ser nula.")
                .NotEmpty().WithMessage("La lista de documentos no puede estar vacía.");

            RuleForEach(x => x.Documentos)
                .SetValidator(new CreateOrigenInvitadoDocumentoDtoValidator());
        }
    }
}
