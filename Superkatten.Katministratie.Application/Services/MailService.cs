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
    private const string CAGEFORM_FILENAME = "kooikaart.pdf";
    private const string SENDER_NAME = "Katministrator";
    private const string SENDER_EMAIL_ADDRESS = "katministratie@superkatten.nl";

    public async Task MailToAsync(string email, string subject, string bodyText, byte[] documentData)
    {
        var message = new MimeMessage();
        message.To.Add(new MailboxAddress("Requester", email));
        message.From.Add(new MailboxAddress(SENDER_NAME, SENDER_EMAIL_ADDRESS));
        message.Subject = subject;
        
        var body = new TextPart(MimeKit.Text.TextFormat.Plain)
        {
            Text = bodyText
        };

        var stream = new MemoryStream(documentData);

        // create an image attachment for the file located at path
        // see: http://ibgwww.colorado.edu/~lessem/psyc5112/usail/mail/mime/typetxt.html
        var attachment = new MimePart(MediaTypeNames.Application.Pdf)
        {
            Content = new MimeContent(stream),
            ContentId = CAGEFORM_FILENAME,
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = CAGEFORM_FILENAME
        };

        message.Body = new Multipart("mixed")
        {
            body,
            attachment
        };

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
    }
}