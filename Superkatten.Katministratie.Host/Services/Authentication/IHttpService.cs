namespace Superkatten.Katministratie.Host.Services.Authentication;

public interface IHttpService
{
    Task<T?> Get<T>(string uri);
    Task<T?> Put<T>(string uri, object value);
    Task<T?> Post<T>(string uri);
    Task<T?> Post<T>(string uri, object value);
    Task Delete(string uri);
}
