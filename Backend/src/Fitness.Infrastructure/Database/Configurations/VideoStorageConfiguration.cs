using Fitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Infrastructure.Database.Configurations;

public class VideoStorageConfiguration : IEntityTypeConfiguration<VideoStorage>
{
    public void Configure(EntityTypeBuilder<VideoStorage> builder)
    {
        builder.ToTable("VideoStorages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.StorageProvider)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Bucket)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Region)
            .HasMaxLength(100);

        builder.Property(x => x.FileKey)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.OriginalFileName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.ContentType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Checksum)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.DurationSeconds);

        builder.Property(x => x.Width);

        builder.Property(x => x.Height);

        builder.Property(x => x.Bitrate);

        builder.Property(x => x.ThumbnailUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.CdnUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        // -------------------------
        // Relationships
        // -------------------------

        builder.HasMany(x => x.ProgramVideos)
            .WithOne(x => x.VideoStorage)
            .HasForeignKey(x => x.VideoStorageId)
            .OnDelete(DeleteBehavior.Restrict);

        // -------------------------
        // Indexes
        // -------------------------

        builder.HasIndex(x => x.FileKey);

        builder.HasIndex(x => new
        {
            x.StorageProvider,
            x.Bucket
        });

        builder.HasIndex(x => x.Status);

        builder.HasIndex(x => x.Checksum);
    }
}