
using Sica.Application.Dto;

namespace Sica.Application.Interfaces
{
    public interface ISicaApiService
    {
        Task<PlayerStatusDto?> StatusPlayer(string document, CancellationToken token);
    }
}
