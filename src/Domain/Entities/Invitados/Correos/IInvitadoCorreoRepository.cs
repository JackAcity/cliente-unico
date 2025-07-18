namespace Domain.Entities.Invitados.Correos
{
    public interface IInvitadoCorreoRepository
    {
        Task<Guid> AddAsync(InvitadoCorreo correo, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<InvitadoCorreo> correos, CancellationToken cancellationToken);
    }
}