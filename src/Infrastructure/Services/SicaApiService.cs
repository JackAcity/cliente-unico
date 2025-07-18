using Microsoft.Extensions.Configuration;
using PlayerRegistration.Infraestructure.Common;
using Sica.Application.Dto;
using Sica.Application.Interfaces;

namespace Sica.Infraestructure.Services
{
    public class SicaApiService : HttpApiServiceBase, ISicaApiService
    {
        private readonly string _baseUrl;

        public SicaApiService(HttpClient httpClient, IConfiguration config)
              : base(httpClient)
        {
            _baseUrl = config["SicaApi:BaseUrl"] ?? throw new ArgumentNullException("CmpApi:BaseUrl not configured");
            _baseUrl += "people";
        }

        public async Task<PlayerStatusDto?> StatusPlayer(string document, CancellationToken token)
        {
            var url = $"{_baseUrl}/{document}/status";
           // Logger.LogInformation($"SicaApiService: url {url}");

            var response = await GetAsync<ResponseDto<PlayerStatusDto>>(url, token);

            if (response == null)
            {
                //Logger.LogWarning("SicaApiService: Respuesta nula al consultar status del jugador.");
                return null;
            }

            if (!response.Ok)
            {
               // Logger.LogWarning($"SicaApiService: Error al consultar status del jugador. StatusCode: {response.StatusCode}, Message: {response.Message}");
                return null;
            }

            return response.Data;
        }


    }

}
