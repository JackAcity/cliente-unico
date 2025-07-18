using Application.Data.Invitados;
using Domain.Entities.Invitados.Origenes;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Invitados.Repositories.Origenes
{
    public class OrigenInvitadoDocumentoRepository : IOrigenInvitadoDocumentoRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public OrigenInvitadoDocumentoRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(OrigenInvitadoDocumento documento, CancellationToken cancellationToken)
        {
            if (documento != null)
            {
                var invitadoResult = await _context.OrigenInvitadoDocumento.AddAsync(documento);
                return documento.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(documento));
            }
        }

        public async Task<bool> AddListAsync(List<OrigenInvitadoDocumento> documentos, CancellationToken cancellationToken)
        {
            if (!documentos.IsNullOrEmpty())
            {
                await _context.OrigenInvitadoDocumento.AddRangeAsync(documentos);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(documentos));
            }
        }
    }
}
