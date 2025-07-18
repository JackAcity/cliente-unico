using ErrorOr;
using MediatR;

namespace Application.Invitados.InvitadoPincipal.Create;

public record CreateDatosPrincipalesCommand(
    string TipoDocumentoCodigo,
    string DocumentoIdentidad,
    string Correo,
    string Numero,
    string Direccion,
    string Referencia
) : IRequest<ErrorOr<Guid>>
{
    public  Guid InvitadoId { get; set; }
}
