using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProService.Domain.Entities;

namespace ImageProService.Infrastructure.Data.EntityConfigs
{
    public class ImageMetadataConfiguration : IEntityTypeConfiguration<ImageMetadata>
    {
        public void Configure(EntityTypeBuilder<ImageMetadata> builder)
        {
            builder.HasKey(im => im.Id);

            builder.Property(im => im.OriginalFileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(im => im.BlobFileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(im => im.ThumbnailBlobFileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(im => im.Url)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(im => im.ThumbnailUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(im => im.Format)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(im => im.AnalysisStatus)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(im => im.AIInsights)
                .HasMaxLength(2000);

            builder.HasIndex(im => im.BlobFileName)
                .IsUnique();

            builder.Property(im => im.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
