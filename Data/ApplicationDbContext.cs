using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Forum.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<ThreadCategory> ThreadCategories { get; set; }
        public DbSet<ThreadGroup> ThreadGroups { get; set; }
        public DbSet<Models.Thread> Threads { get; set; } // avoiding ambiguity 
        public DbSet<ThreadReply> ThreadReplies { get; set; }
        public DbSet<User> Users { get; set; }

        // override default behaviours so if user is deleted, their replies and threads aren't deleted. TODO: Test with seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Thread>()
                .HasOne(t => t.Author) // thread has one author (User)
                .WithMany(u => u.ThreadsAuthored) // User can have many threads
                .HasForeignKey(t => t.AuthorId) // FK 
                .OnDelete(DeleteBehavior.SetNull); // set thread author to null if user is deleted

            modelBuilder.Entity<ThreadReply>()
                .HasOne(t => t.Author) // reply has one author (User)
                .WithMany(u => u.ThreadReplies) // User can have many replies
                .HasForeignKey(t => t.AuthorId) // FK 
                .OnDelete(DeleteBehavior.SetNull); // set reply author to null if user is deleted
        }
    }
}
