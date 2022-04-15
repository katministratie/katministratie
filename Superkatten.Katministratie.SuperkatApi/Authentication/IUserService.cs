namespace Superkatten.Katministratie.SuperkatApi.Authentication
{
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);
    }
}
