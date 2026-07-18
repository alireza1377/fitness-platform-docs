using Fitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Infrastructure.Database.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TokenHash)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.DeviceId)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.UserAgent)
            .HasMaxLength(500);

        builder.Property(x => x.IpAddress)
            .HasMaxLength(100);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.TokenHash)
            .IsUnique();
    }
}