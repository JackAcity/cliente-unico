using Application.Data.Invitados;
using Domain.Entities.Invitados.Documentos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Invitados.Repositories.Documentos
{
    public class InvitadoDocumentoRepository : IInvitadoDocumentoRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public InvitadoDocumentoRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(InvitadoDocumento invitadoDocumento, CancellationToken cancellationToken)
        {
            if (invitadoDocumento != null)
            {
                var invitadoResult = await _context.InvitadoDocumento.AddAsync(invitadoDocumento);
                return invitadoDocumento.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(invitadoDocumento));
            }
        }

        public async Task<bool> AddListAsync(List<InvitadoDocumento> invitadoDocumento, CancellationToken cancellationToken)
        {
            if (!invitadoDocumento.IsNullOrEmpty())
            {
                await _context.InvitadoDocumento.AddRangeAsync(invitadoDocumento);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(invitadoDocumento));
            }
        }
    }
}
