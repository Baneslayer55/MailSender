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
        public void Send(Mail mail) 
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress("","")); // взять с jsona            
        }
    }
}
