using MailKit.Net.Smtp;
using MailKit.Security;
using MailSender.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;


namespace MailSender.Services
{
    public class MailService
    {
        public static void Send(Mail mail) 
        {
            MimeMessage message = new MimeMessage();

            message.Sender = MailboxAddress.Parse("brisa.bosco@ethereal.email");

            foreach(string email in mail.Recipients)
            {
                message.To.Add(MailboxAddress.Parse(email));
            }
            
            message.Subject = "Test Email Subject";

            message.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };

            
            using var smtp = new SmtpClient();

            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);

            smtp.Authenticate("brisa.bosco@ethereal.email", "AArwGt1Dhr45q9GCUv");
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
