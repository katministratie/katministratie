using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services;

public interface IMailService
{
    Task<bool> MailToAsync(string email, string subject, string bodyText, byte[] documentData);
}
