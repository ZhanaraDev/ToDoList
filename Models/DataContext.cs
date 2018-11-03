using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DataContext: DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<Task> Task { get; set; }

        public DbSet<TaskCategory> TaskCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasOne(a => a.Profile)
            .WithOne(b => b.User)
            .HasForeignKey<UserProfile>(b => b.UserRef);

            modelBuilder.Entity<Task>()
            .HasOne(e => e.TaskCategory)
            .WithMany(c => c.Tasks);


        }
    }
}
