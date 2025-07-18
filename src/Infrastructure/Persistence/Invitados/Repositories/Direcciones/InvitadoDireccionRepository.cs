using Application.Data.Invitados;
using Domain.Entities.Invitados.Direcciones;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Invitados.Repositories.Direcciones
{
    public class InvitadoDireccionRepository : IInvitadoDireccionRepository
    {
        private readonly IApplicationInvitadosDbContext _context;

        public InvitadoDireccionRepository(IApplicationInvitadosDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(InvitadoDireccion invitadoDireccion, CancellationToken cancellationToken)
        {
            if (invitadoDireccion != null)
            {
                var invitadoResult = await _context.InvitadoDireccion.AddAsync(invitadoDireccion);
                return invitadoDireccion.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(invitadoDireccion));
            }
        }

        public async Task<bool> AddListAsync(List<InvitadoDireccion> invitadoDireccion, CancellationToken cancellationToken)
        {
            if (!invitadoDireccion.IsNullOrEmpty())
            {
                await _context.InvitadoDireccion.AddRangeAsync(invitadoDireccion);
                return true;
            }
            else
            {
                throw new ArgumentNullException(nameof(invitadoDireccion));
            }
        }
    }
}
