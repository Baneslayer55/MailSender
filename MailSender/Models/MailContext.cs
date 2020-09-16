using Microsoft.EntityFrameworkCore;
using System;

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
