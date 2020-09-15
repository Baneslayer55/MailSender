using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailSender.Models
{
    public class Mail
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
                
        public string[] Recipients { get; set; }
    }
}
