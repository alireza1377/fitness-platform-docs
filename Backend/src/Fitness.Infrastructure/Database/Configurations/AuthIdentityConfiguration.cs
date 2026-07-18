using Fitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Infrastructure.Database.Configurations;

public class AuthIdentityConfiguration : IEntityTypeConfiguration<AuthIdentity>
{
    public void Configure(EntityTypeBuilder<AuthIdentity> builder)
    {
        builder.ToTable("AuthIdentities");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsPhoneVerified)
            .IsRequired();

        builder.Property(x => x.IsBlocked)
            .IsRequired();

        builder.Property(x => x.FailedAttempts)
            .IsRequired();

        builder.HasIndex(x => x.UserId)
            .IsUnique();
    }
}