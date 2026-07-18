using Fitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            
            .HasMaxLength(200);

        builder.HasIndex(x => x.Email)
            .IsUnique();

       builder.Property(x => x.PhoneNumber)
    .IsRequired()
    .HasMaxLength(20);

        builder.HasIndex(x => x.PhoneNumber)
            .IsUnique();

        

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.IsVerified)
            .HasDefaultValue(false);
    }
}