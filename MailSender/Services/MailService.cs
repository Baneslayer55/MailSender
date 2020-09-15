using MailKit.Net.Smtp;
using MailKit.Security;
using MailSender.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Text.RegularExpressions;

namespace MailSender.Services
{
    /// <summary>
    /// This class is responsible for sending emails.
    /// The MailKit library is used internally.
    /// </summary>
    public class MailService
    {
        /// <summary>
        /// This static method sending emails.
        /// The MailKit library is used internally.
        /// </summary>
        public static void Send(Mail mail) 
        {
            MimeMessage message = new MimeMessage();

            message.Sender = MailboxAddress.Parse("brisa.bosco@ethereal.email");
            //Mimekit internal template does not handle well mails, so i have implemented an additional check
            string emailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            foreach (string email in mail.Recipients)
            {
                if (Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase))
                {
                    message.To.Add(MailboxAddress.Parse(email));
                }   
                else
                {
                    mail.ErrorMessage += email + " is invalid;";
                }
                
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
