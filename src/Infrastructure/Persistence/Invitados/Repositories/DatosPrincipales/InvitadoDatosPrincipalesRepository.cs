using Application.Data.Invitados;
using Domain.Entities.Invitados.DatosPrincipales;

namespace Infrastructure.Persistence.Invitados.Repositories.DatosPrincipales
{
    public class InvitadoDatosPrincipalesRepository : IInvitadoDatosPrincipalesRepository
    {
        
    private readonly IApplicationInvitadosDbContext _context;

        public InvitadoDatosPrincipalesRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(InvitadoDatosPrincipales datosPrincipales)
        {
            if (datosPrincipales != null)
            {
                var invitadoResult = await _context.InvitadoDatosPrincipales.AddAsync(datosPrincipales);
                return datosPrincipales.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(datosPrincipales));
            }
        }
    }
}
