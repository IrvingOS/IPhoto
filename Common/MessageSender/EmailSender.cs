using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Threading.Tasks;

namespace IPhoto.Common.MessageSender
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return SendMail(email, subject, htmlMessage);
        }

        public static Task SendMail(string email, string subject, string htmlMessage)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("IPhoto", "iphoto@irvingsoft.top"));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = htmlMessage,

            };
            message.Priority = MessagePriority.Urgent;

            SmtpClient client = new();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect("smtp.exmail.qq.com", 465, true);
            client.Authenticate("iphoto@irvingsoft.top", "password");

            return client.SendAsync(message);
        }
    }
}
