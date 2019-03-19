using System;
using Microsoft.EntityFrameworkCore;
using rencredit_task.Models;

namespace rencredit_task
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Office> Offices { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // я использовал sqlite чтобы не ставить sql server (у меня линукс), 
            // но т.к. это ORM - то поменять можно одной строкой
            optionsBuilder.UseSqlite("Filename=Rencredit.db");
        }
    }
}