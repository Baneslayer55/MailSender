using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace MailSender.Models
{
    public class MailContext : DbContext
    {
        public DbSet<Mail> Mails { get; set; }
        public MailContext(DbContextOptions<MailContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
