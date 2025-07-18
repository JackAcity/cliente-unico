namespace Domain.Entities.Invitados.Telefonos
{
    public interface IInvitadoTelefonoRepository
    {
        Task<Guid> AddAsync(InvitadoTelefono telefono, CancellationToken cancellationToken);

        Task<bool> AddListAsync(List<InvitadoTelefono> telefonos, CancellationToken cancellationToken);
    }
}
