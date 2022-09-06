using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services;

public interface IMailService
{
    Task MailToAsync(string email, string subject, string bodyText, byte[] documentData);
}
