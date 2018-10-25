using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<Task> Task { get; set; }

        public DbSet<TaskCategory> TaskCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasOne(a => a.profile)
            .WithOne(b => b.user)
            .HasForeignKey<UserProfile>(b => b.UserRef);

            modelBuilder.Entity<Task>()
            .HasOne(e => e.TaskCategory)
            .WithMany(c => c.Tasks);


        }
    }
}
