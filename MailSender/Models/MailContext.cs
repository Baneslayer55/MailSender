using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mail>()
                        .Property(p => p.Recipients)
                        .HasConversion(
                            v => string.Join(',', v),
                            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }

        public MailContext(DbContextOptions<MailContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
