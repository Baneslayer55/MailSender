using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailSender.Models;
using MailSender.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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
        /// Return all serialized mail entities. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mail>>> Get()
        {
            return await db.Mails.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <example>Input entity template:</example>
        /// <code>
        /// 
        /// 
        /// </code>
        /// <param name="mail"> </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Mail>> Post(Mail mail)
        {
            if (mail == null)
            {
                return BadRequest();
            }

            try
            {
                MailService.Send(mail);
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
