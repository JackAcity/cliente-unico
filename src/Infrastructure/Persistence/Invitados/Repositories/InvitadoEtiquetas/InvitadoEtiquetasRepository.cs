using Application.Data.Invitados;
using Domain.Entities.Invitados.EtiquetaInvitados;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.Etiquetas
{
    public class InvitadoEtiquetasRepository : IInvitadoEtiquetaRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public InvitadoEtiquetasRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<long> AddAsync(InvitadoEtiqueta invitadoEtiqueta, CancellationToken cancellationToken)
        {
            if (invitadoEtiqueta != null)
            {
                var invitadoResult = await _context.InvitadoEtiqueta.AddAsync(invitadoEtiqueta);
                return invitadoEtiqueta.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(invitadoEtiqueta));
            }
        }

        public async Task<bool> AddListAsync(List<InvitadoEtiqueta> invitadoEtiqueta, CancellationToken cancellationToken)
        {
            if (!invitadoEtiqueta.IsNullOrEmpty())
            {
                await _context.InvitadoEtiqueta.AddRangeAsync(invitadoEtiqueta);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(invitadoEtiqueta));
            }
        }
    }
}
