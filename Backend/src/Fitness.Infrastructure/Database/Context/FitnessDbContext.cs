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

    public DbSet<Payment> Payments => Set<Payment>();

    public DbSet<Notification> Notifications => Set<Notification>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<FitnessProgram> FitnessPrograms => Set<FitnessProgram>();

    public DbSet<VideoStorage> VideoStorages => Set<VideoStorage>();

    public DbSet<ProgramVideo> ProgramVideos => Set<ProgramVideo>();

    public DbSet<UserProgramProgress> UserProgramProgresses
        => Set<UserProgramProgress>();

    public DbSet<UserVideoProgress> UserVideoProgresses
        => Set<UserVideoProgress>();

    public DbSet<UserStatistics> UserStatistics
        => Set<UserStatistics>();

    public DbSet<SubscriptionPlan> SubscriptionPlans
        => Set<SubscriptionPlan>();

    public DbSet<Subscription> Subscriptions
        => Set<Subscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitnessDbContext).Assembly);

        //----------------------------------------------------
        // Category -> Programs
        //----------------------------------------------------

        modelBuilder.Entity<Category>()
            .HasMany(x => x.Programs)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        //----------------------------------------------------
        // Program -> Videos
        //----------------------------------------------------

        modelBuilder.Entity<FitnessProgram>()
            .HasMany(x => x.Videos)
            .WithOne(x => x.FitnessProgram)
            .HasForeignKey(x => x.FitnessProgramId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProgramVideo>()
            .HasIndex(x => new
            {
                x.FitnessProgramId,
                x.Order
            })
            .IsUnique();

        //----------------------------------------------------
        // User -> Video Progress
        //----------------------------------------------------

        modelBuilder.Entity<UserVideoProgress>()
            .HasOne(x => x.User)
            .WithMany(x => x.VideoProgresses)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserVideoProgress>()
            .HasOne(x => x.ProgramVideo)
            .WithMany(x => x.Progresses)
            .HasForeignKey(x => x.ProgramVideoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserVideoProgress>()
            .HasIndex(x => new
            {
                x.UserId,
                x.ProgramVideoId
            })
            .IsUnique();

        //----------------------------------------------------
        // User -> Program Progress
        //----------------------------------------------------

        modelBuilder.Entity<UserProgramProgress>()
            .HasOne(x => x.User)
            .WithMany(x => x.ProgramProgresses)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserProgramProgress>()
            .HasOne(x => x.FitnessProgram)
            .WithMany(x => x.UserProgresses)
            .HasForeignKey(x => x.FitnessProgramId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserProgramProgress>()
            .HasIndex(x => new
            {
                x.UserId,
                x.FitnessProgramId
            })
            .IsUnique();

        //----------------------------------------------------
        // User Statistics
        //----------------------------------------------------

        modelBuilder.Entity<UserStatistics>()
            .HasOne(x => x.User)
            .WithOne(x => x.Statistics)
            .HasForeignKey<UserStatistics>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}