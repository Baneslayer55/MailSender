using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailSender.Models;
using Microsoft.AspNetCore.Http;
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

            if (!db.Mails.Any())
            {
                db.Mails.Add(new Mail { Body = "test", Recipients = "a", Subject = "test" });
                db.Mails.Add(new Mail { Body = "test2", Recipients = "d", Subject = "test2" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mail>>> Get()
        {
            return await db.Mails.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Mail>> Post(Mail mail)
        {
            if (mail == null)
            {
                return BadRequest();
            }

            db.Mails.Add(mail);
            await db.SaveChangesAsync();
            return Ok(mail);
        }
    }
}
