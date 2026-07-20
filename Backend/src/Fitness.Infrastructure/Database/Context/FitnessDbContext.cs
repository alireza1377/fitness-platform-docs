using Fitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Database.Context;

public class FitnessDbContext : DbContext
{
    public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<AuthIdentity> AuthIdentities => Set<AuthIdentity>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<FitnessProgram> FitnessPrograms => Set<FitnessProgram>();

    public DbSet<ProgramVideo> ProgramVideos => Set<ProgramVideo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitnessDbContext).Assembly);

        // Category -> FitnessPrograms
        modelBuilder.Entity<Category>()
            .HasMany(x => x.Programs)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // FitnessProgram -> Videos
        modelBuilder.Entity<FitnessProgram>()
            .HasMany(x => x.Videos)
            .WithOne(x => x.FitnessProgram)
            .HasForeignKey(x => x.FitnessProgramId)
            .OnDelete(DeleteBehavior.Cascade);

        // ترتیب ویدئوها داخل هر برنامه باید یکتا باشد
        modelBuilder.Entity<ProgramVideo>()
            .HasIndex(x => new
            {
                x.FitnessProgramId,
                x.Order
            })
            .IsUnique();
    }
}