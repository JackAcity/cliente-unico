using Application.Data.Invitados;
using Domain.Entities.Invitados.Origenes;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.Origenes
{
    public class OrigenAdicionalRepository : IOrigenAdicionalRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public OrigenAdicionalRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(OrigenAdicional origenAdicional, CancellationToken cancellationToken)
        {
            if (origenAdicional != null)
            {
                var invitadoResult = await _context.OrigenAdicional.AddAsync(origenAdicional);
                return origenAdicional.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(origenAdicional));
            }
        }

        public async Task<bool> AddListAsync(List<OrigenAdicional> origenesAdicionales, CancellationToken cancellationToken)
        {
            if (!origenesAdicionales.IsNullOrEmpty())
            {
                await _context.OrigenAdicional.AddRangeAsync(origenesAdicionales);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(origenesAdicionales));
            }
        }
    }
}
