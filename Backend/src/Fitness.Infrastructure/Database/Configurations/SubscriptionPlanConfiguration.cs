using Fitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Infrastructure.Database.Configurations;

public class SubscriptionPlanConfiguration
    : IEntityTypeConfiguration<SubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.ToTable("SubscriptionPlans");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Price)
            .HasPrecision(18,2);

        builder.Property(x => x.DurationDays);

        builder.Property(x => x.DisplayOrder);

        builder.Property(x => x.IsActive);
    }
}