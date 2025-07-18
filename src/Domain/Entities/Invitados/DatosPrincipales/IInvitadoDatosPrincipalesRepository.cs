namespace Domain.Entities.Invitados.DatosPrincipales
{
    public interface IInvitadoDatosPrincipalesRepository
    {
        Task<Guid> AddAsync(InvitadoDatosPrincipales datosPrincipales);
    }
}
