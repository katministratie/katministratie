using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Superkatten.Katministratie.Application.Helpers;
using System;
using System.IO;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services;

public class MailService : IMailService
{
    public async Task MailToAsync(string email, string subject, string content, byte[] documentData)
    {
        var message = new MimeMessage();
        message.To.Add(new MailboxAddress("Requester", email));
        message.From.Add(new MailboxAddress("Katministrator", "katministratie@superkatten.nl"));
        message.Subject = subject;
        
        var body = new TextPart("plain")
        {
            Text = content
        };

        var stream = new MemoryStream(documentData);
        var filename = "kooikaart.pdf";
        // create an image attachment for the file located at path
        // see: http://ibgwww.colorado.edu/~lessem/psyc5112/usail/mail/mime/typetxt.html
        var attachment = new MimePart(MediaTypeNames.Application.Pdf)
        {
            Content = new MimeContent(stream), //File.OpenRead(fileName), ContentEncoding.Default),
            ContentId = filename,
            //ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = filename //Path.GetFileName(filename)
        };

        // now create the multipart/mixed container to hold the message text and the
        // image attachment
        var multipart = new Multipart("mixed")
        { 
            body,
            attachment
        };

        // now set the multipart/mixed as the message body
        message.Body = multipart;

        using var client = new SmtpClient();
        client.Connect(
            EmailSettings.SmtpHost,
            EmailSettings.SmtpPortNumber,
            SecureSocketOptions.Auto);
        client.Authenticate(
            ClientSecrets.GmailClientId,
            ClientSecrets.GmailClientSecret);

        var result = await client.SendAsync(message);

        client.Disconnect(true);

        //File.Delete(fileName);
    }
}