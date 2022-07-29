
using System.Threading.Tasks;
using System.Net.Mail;
using System;
using System.ComponentModel;

namespace Superkatten.Katministratie.Application.Services;

public class MailService : IMailService
{
    static bool mailSent = false;
    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        var token = (string)e.UserState;

        if (e.Cancelled)
        {
            Console.WriteLine("[{0}] Send canceled.", token);
        }
        if (e.Error != null)
        {
            Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
        }
        else
        {
            Console.WriteLine("Message sent.");
        }
        mailSent = true;
    }

    public Task MailToAsync(string email, string data)
    {
        // Command-line argument must be the SMTP host.
        SmtpClient client = new SmtpClient("smtp-relay.gmail.com", 25);
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        MailAddress from = new MailAddress("katministratie@superkatten.nl",
           "Katministrator",
        System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress("trudy@sfact.nl");
        // Specify the message content.
        MailMessage message = new MailMessage(from, to);
        message.Body = "This is a test email message sent by an application. ";
        // Include some non-ASCII characters in body and subject.
        string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
        message.Body += Environment.NewLine + someArrows + data;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = "Requested report" + someArrows;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        // Set the method that is called back when the send operation ends.
        client.SendCompleted += new
        SendCompletedEventHandler(SendCompletedCallback);
        // The userState can be any object that allows your callback
        // method to identify this send operation.
        // For this example, the userToken is a string constant.
        string userState = "Sending message";
        client.SendAsync(message, userState);

        return Task.CompletedTask;
    }
}
