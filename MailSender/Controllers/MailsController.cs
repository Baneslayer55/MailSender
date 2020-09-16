using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailSender.Models;
using MailSender.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        MailContext db;

        public MailsController(MailContext context)
        {
            db = context;
        }

        /// <summary>
        /// Returns the history of sent messages.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mail>>> GetMailHistory()
        {
            return await db.Mails.ToListAsync();
        }


        /// <summary>
        /// Sends messages to the recipient's list.
        /// Accepts json in the body of the http post request, 
        /// consisting of the message body, message subject, 
        /// and recipient's list.
        /// </summary>
        /// <example>Input entity template:</example>
        /// <code>
        /// {
        ///     "subject": "Subject text",
        ///     "body": "Body text",
        ///     "recipients": ["example@provider.domain", "example2@provider.domain"]
        /// } 
        /// </code>  
        [HttpPost]
        public async Task<ActionResult<Mail>> SendMails(Mail mail)
        {
            if (mail == null)
            {
                return BadRequest();
            }

            try
            {
                MailService.Send(mail, "mailconfig.json");
                mail.IsSended = true;
            }
            catch (Exception e)
            {
                mail.ErrorMessage += " " + e.Message;
                mail.IsSended = false;              
            }

            mail.SendingDateTime = DateTime.Now;
            
            db.Mails.Add(mail);
            await db.SaveChangesAsync();
            return Ok(mail);
        }
    }
}
