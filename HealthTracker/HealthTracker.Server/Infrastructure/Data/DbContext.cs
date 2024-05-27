using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Modules.Community.Models;
using HealthTracker.Server.Modules.Health.Models;
using HealthTracker.Server.Modules.Meals.Models;
using HealthTracker.Server.Modules.PhysicalActivity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Infrastrucure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Friendship> Friendship { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<HealthMeasurement> HealthMeasurement { get; set; }
        public DbSet<MeasurementType> MeasurementType { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<MealType> MealType { get; set; }
        public DbSet<MealUser> MealUser { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<ExerciseType> ExerciseType { get; set; }
        public DbSet<Goal> Goal { get; set; }
        public DbSet<GoalType> GoalType { get; set; }
        public DbSet<Workout> Workout { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Like> Like { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User1)
                .WithMany(u => u.Friendships)
                .HasForeignKey(f => f.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User2)
                .WithMany()
                .HasForeignKey(f => f.User2Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Status>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.UserId, l.PostId });


        }

    }
}
