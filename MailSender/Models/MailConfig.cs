using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSender.Models
{
    public class MailConfig
    {
        private static MailConfig instance;

        public static MailConfig GetMailConfig()
        {
            if (instance == null)
            {
                //instance = Js;
            }
            return instance;
        }

    }
}
