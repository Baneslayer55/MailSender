using Microsoft.EntityFrameworkCore;
using System;

namespace MailSender.Models
{
    public class MailContext : DbContext
    {
        /// <summary>
        /// This property shows Entity framework 
        /// which entity needs a table in the database.
        /// </summary>
        public DbSet<Mail> Mails { get; set; }
                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Соглашение о чтении и записи свойства Recipients
            // записывается в бд как join-еный массив строк, достается как массив строк
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
