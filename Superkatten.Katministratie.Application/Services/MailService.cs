using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services;

public class MailService : IMailService
{
    public Task MailToAsync(string email, string data)
    {

        var message = new MimeMessage();
        message.To.Add(new MailboxAddress("Requester", email));
        message.From.Add(new MailboxAddress("Katministrator", "katministratie@superkatten.nl"));
        message.Subject = "Inventarisatieformulier Wakker Dier";
        message.Body = new TextPart("plain")
        {
            Text = data
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.Auto);
            client.Authenticate("johandekroon@gmail.com", "pofhcoxtzxkkejxc");
            try
            {
                var result = client.Send(message);
            }
            catch(Exception x)
            {
                //todo; hoe deze zichtbaar maken in UI
            }
            client.Disconnect(true);
        }

        return Task.CompletedTask;
    }
}