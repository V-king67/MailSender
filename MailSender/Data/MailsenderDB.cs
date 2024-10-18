﻿using MailSender.lib.Models;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Data
{
    internal class MailsenderDB : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<SchedulerTask> SchedulerTasks {  get; set; }

        public MailsenderDB(DbContextOptions options) : base(options) { }

        //Каскадное удаление
        //protected override void OnModelCreating(ModelBuilder db)
        //{
        //    db.Entity<SchedulerTask>()
        //        .HasMany(t => t.Recipients)
        //        .WithOne("Task")
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
