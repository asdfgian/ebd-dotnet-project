using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.Out;

namespace WebApiEbd.Infrastructure.Http
{
    public class DecolectaApiProvider(
        HttpClient client,
        IConfiguration configuration) : IDecolectaApiProvider
    {
        public async Task<HttpResponseMessage> GetDetailAsync(string ruc)
        {
            var baseUrl = configuration["ExternalApis:Sunat:BaseUrl"];
            var token = configuration["ExternalApis:Sunat:Token"];
            var url = $"{baseUrl}/ruc?numero={ruc}";
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await client.SendAsync(request);
        }
    }
}