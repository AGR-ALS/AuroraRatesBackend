using AuroraRates.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuroraRates.DataAccess.Configurations;

public class ReviewsConfiguration : IEntityTypeConfiguration<ReviewEntity>
{
    public void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        builder.ToTable("Reviews");
        builder.HasKey(r => r.Id);
        builder.HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId);
        builder.HasOne(r => r.MediaType).WithMany().HasForeignKey(r => r.MediaTypeId);
    }
}