using Fitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Database.Context;

public class FitnessDbContext : DbContext
{
    public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
        : base(options)
    {
    }
public DbSet<UserVideoProgress> UserVideoProgresses =>
    Set<UserVideoProgress>();
    public DbSet<User> Users => Set<User>();

    public DbSet<AuthIdentity> AuthIdentities => Set<AuthIdentity>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<FitnessProgram> FitnessPrograms => Set<FitnessProgram>();

    public DbSet<ProgramVideo> ProgramVideos => Set<ProgramVideo>();

public DbSet<UserProgramProgress> UserProgramProgresses => Set<UserProgramProgress>();

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
            
            // User -> Video Progress
modelBuilder.Entity<UserVideoProgress>()
    .HasOne(x => x.User)
    .WithMany()
    .HasForeignKey(x => x.UserId)
    .OnDelete(DeleteBehavior.Cascade);

// ProgramVideo -> Progresses
modelBuilder.Entity<UserVideoProgress>()
    .HasOne(x => x.ProgramVideo)
    .WithMany(x => x.Progresses)
    .HasForeignKey(x => x.ProgramVideoId)
    .OnDelete(DeleteBehavior.Cascade);

// هر کاربر برای هر ویدئو فقط یک رکورد پیشرفت داشته باشد
modelBuilder.Entity<UserVideoProgress>()
    .HasIndex(x => new
    {
        x.UserId,
        x.ProgramVideoId
    })
    .IsUnique();
    }
}