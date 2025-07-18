using Application.Data.Invitados;
using Domain.Entities.Invitados.Notas;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Invitados.Repositories.Notas
{
    public class InvitadoNotasRepository : IInvitadoNotaRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public InvitadoNotasRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<long> AddAsync(InvitadoNota nota, CancellationToken cancellationToken)
        {
            if (nota != null)
            {
                var invitadoResult = await _context.InvitadoNota.AddAsync(nota);
                return nota.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(nota));
            }
        }

        public async Task<bool> AddListAsync(List<InvitadoNota> notas, CancellationToken cancellationToken)
        {
            if (!notas.IsNullOrEmpty())
            {
                await _context.InvitadoNota.AddRangeAsync(notas);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(notas));
            }
        }
    }
}
