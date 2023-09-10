namespace Superkatten.Katministratie.Api.Client.Services;

public interface IHttpService
{
    Task<T?> Get<T>(string uri);
}