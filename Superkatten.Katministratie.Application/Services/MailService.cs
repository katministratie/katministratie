using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services;

public class MailService : IMailService
{
    public Task MailToAsync(string email, string subject, string content, string fileName)
    {
        var message = new MimeMessage();
        message.To.Add(new MailboxAddress("Requester", email));
        message.From.Add(new MailboxAddress("Katministrator", "katministratie@superkatten.nl"));
        message.Subject = subject;
        
        var body = new TextPart("plain")
        {
            Text = content
        };

        // create an image attachment for the file located at path
        // see: http://ibgwww.colorado.edu/~lessem/psyc5112/usail/mail/mime/typetxt.html
        var attachment = new MimePart("application", "pdf")
        {
            Content = new MimeContent(File.OpenRead(fileName), ContentEncoding.Default),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = Path.GetFileName(fileName)
        };

        // now create the multipart/mixed container to hold the message text and the
        // image attachment
        var multipart = new Multipart("mixed");
        multipart.Add(body);
        multipart.Add(attachment);

        // now set the multipart/mixed as the message body
        message.Body = multipart;

        using (var client = new SmtpClient())
        {
            //TODO: tijdelijk gmail account login gebruiken
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

        Thread.Sleep(5000);

        File.Delete(fileName);

        return Task.CompletedTask;
    }
}