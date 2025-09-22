namespace WebApiEbd.Core.Application.Ports.Out
{
    public interface IDecolectaApiProvider
    {
        Task<HttpResponseMessage> GetDetailAsync(string ruc);
    }
    
}