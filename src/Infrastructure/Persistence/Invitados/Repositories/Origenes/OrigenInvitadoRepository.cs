using Application.Data.Invitados;
using Domain.Entities.Invitados.Origenes;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.Origenes
{
    public class OrigenInvitadoRepository : IOrigenInvitadoRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public OrigenInvitadoRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async  Task<Guid> AddAsync(OrigenInvitado invitado, CancellationToken cancellationToken)
        {
            if (invitado != null)
            {
                var invitadoResult = await _context.OrigenInvitado.AddAsync(invitado);
                return invitado.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(invitado));
            }
        }

        public async Task<bool> AddListAsync(List<OrigenInvitado> invitados, CancellationToken cancellationToken)
        {
            if (!invitados.IsNullOrEmpty())
            {
                await _context.OrigenInvitado.AddRangeAsync(invitados);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(invitados));
            }
        }
    }
}
