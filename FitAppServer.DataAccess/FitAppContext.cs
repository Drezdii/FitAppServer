using FitAppServer.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace FitAppServer.DataAccess
{
    public class FitAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseInfo> ExerciseInfo { get; set; }
        public DbSet<Set> Sets { get; set; }

        public FitAppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<Workout>(u => u.Workouts)
                .WithOne(w => w.User)
                .IsRequired(true);

            modelBuilder.Entity<Workout>()
                .HasMany<Exercise>(w => w.Exercises)
                .WithOne(e => e.Workout)
                .IsRequired(true);

            modelBuilder.Entity<Exercise>()
                .HasOne<ExerciseInfo>(q => q.ExerciseInfo)
                .WithMany(q => q.Exercises)
                .IsRequired(true);

            modelBuilder.Entity<Exercise>()
                .HasMany<Set>(s => s.Sets)
                .WithOne(q => q.Exercise)
                .IsRequired(true);
        }
    }
}
