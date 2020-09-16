using MailKit.Net.Smtp;
using MailKit.Security;
using MailSender.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MailSender.Services
{
    /// <summary>
    /// This class is responsible for sending emails.
    /// The MailKit library is used internally.
    /// </summary>
    public static class MailService
    {
        /// <summary>
        /// Static method wich sending mails based on MailKit.   
        /// Due to the insufficiently reliable email validation in MailKit, 
        /// an additional validation step is implemented in this method.
        /// The IsSended field of Mail entity is set to true if at least one message was sent.
        /// All emails that did not pass validation will be written to the ErrorMessage field of Mail entity.        
        /// </summary>
        /// <param name="mail"> entity wich deserialized by controller.</param>
        public static void Send(Mail mail) 
        {
            MimeMessage message = new MimeMessage();            

            MailConfig mailConfig = MailConfig.GetConfig("mailconfig.json");

            message.Sender = MailboxAddress.Parse(mailConfig.User);
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
            
            message.Subject = mail.Subject;

            message.Body = new TextPart(TextFormat.Plain) { Text = mail.Body };
            
            using var smtp = new SmtpClient();

            smtp.Connect(mailConfig.Host, Convert.ToInt32(mailConfig.Port), SecureSocketOptions.StartTls);

            smtp.Authenticate(mailConfig.User, mailConfig.Password);
            smtp.Send(message);
            smtp.Disconnect(true);
        }       
    }
}
