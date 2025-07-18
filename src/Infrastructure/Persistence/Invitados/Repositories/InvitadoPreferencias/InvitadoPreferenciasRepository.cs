using Application.Data.Invitados;
using Domain.Entities.Invitados.InvitadoPreferencias;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.InvitadoPreferencias
{
    public class InvitadoPreferenciasRepository : IInvitadoPreferenciaRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public InvitadoPreferenciasRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<long> AddAsync(InvitadoPreferencia invitadoPreferencia, CancellationToken cancellationToken)
        {
            if (invitadoPreferencia != null)
            {
                var invitadoResult = await _context.InvitadoPreferencia.AddAsync(invitadoPreferencia);
                return invitadoPreferencia.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(invitadoPreferencia));
            }
        }

        public async Task<bool> AddListAsync(List<InvitadoPreferencia> invitadoPreferencias, CancellationToken cancellationToken)
        {
            if (!invitadoPreferencias.IsNullOrEmpty())
            {
                await _context.InvitadoPreferencia.AddRangeAsync(invitadoPreferencias);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(invitadoPreferencias));
            }
        }
    }
}
