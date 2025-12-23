using Microsoft.EntityFrameworkCore;
using TaskMaster.API.Entities;

namespace TaskMaster.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets will go here
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskStatusHistory> TaskStatusHistories { get; set; }

        // Relationship configuration will go here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // User → TaskItem (One-to-Many)
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);
            // TaskItem → TaskStatusHistory (One-to-Many)
            modelBuilder.Entity<TaskStatusHistory>()
                .HasOne(h => h.TaskItem)
                .WithMany(t => t.StatusHistory)
                .HasForeignKey(h => h.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);
            // User → TaskStatusHistory (One-to-Many)
            modelBuilder.Entity<TaskStatusHistory>()
                .HasOne(h => h.ChangedByUser)
                .WithMany(u => u.StatusHistories)
                .HasForeignKey(h => h.ChangedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
