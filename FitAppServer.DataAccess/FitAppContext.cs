using FitAppServer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitAppServer.DataAccess;

public class FitAppContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Workout> Workouts { get; set; } = null!;
    public DbSet<Exercise> Exercises { get; set; } = null!;
    public DbSet<ExerciseInfo> ExerciseInfo { get; set; } = null!;
    public DbSet<Set> Sets { get; set; } = null!;
    public DbSet<OneRepMax> OneRepMaxes { get; set; } = null!;

    public FitAppContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Workouts)
            .WithOne(w => w.User)
            .IsRequired();

        modelBuilder.Entity<Workout>()
            .HasMany(w => w.Exercises)
            .WithOne(e => e.Workout)
            .IsRequired();

        modelBuilder.Entity<Exercise>()
            .HasOne(e => e.ExerciseInfo)
            .WithMany(q => q.Exercises)
            .IsRequired();

        modelBuilder.Entity<Exercise>()
            .HasMany(e => e.Sets)
            .WithOne(s => s.Exercise)
            .IsRequired();
    }
}