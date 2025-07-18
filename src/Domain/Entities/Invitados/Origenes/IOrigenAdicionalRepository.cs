using Domain.Entities.Invitados.Correos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Invitados.Origenes
{
    public interface IOrigenAdicionalRepository
    {
        Task<Guid> AddAsync(OrigenAdicional origenAdicional, CancellationToken cancellationToken);
        Task<bool> AddListAsync(List<OrigenAdicional> origenesAdicionales, CancellationToken cancellationToken);
    }
}
