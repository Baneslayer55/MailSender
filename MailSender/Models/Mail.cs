using System;

namespace MailSender.Models
{
    public class Mail
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
                
        public string[] Recipients { get; set; }

        public bool IsSended { get; set; } = false;

        public DateTime SendingDateTime { get; set; }

        public string ErrorMessage { get; set; } = "";
    }
}
