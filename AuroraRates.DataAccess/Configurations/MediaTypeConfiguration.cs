using AuroraRates.DataAccess.Entity;
using AuroraRates.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuroraRates.DataAccess.Configurations;

public class MediaTypeConfiguration : IEntityTypeConfiguration<MediaTypeEntity>
{
    public void Configure(EntityTypeBuilder<MediaTypeEntity> builder)
    {
        builder.ToTable("MediaTypes");
        builder.HasKey(m => m.Id);
        builder.HasData(
            new MediaTypeEntity() {Id = new Guid("e9956f89-e020-4ae4-a190-f95d4afa5788"), Name = "Movie"},
            new MediaTypeEntity() {Id = new Guid("75f391d8-4f36-4346-8574-9db7515598f9"), Name = "Game"},
            new MediaTypeEntity() {Id = new Guid("7dc36adb-99f3-49d7-ae19-5dc35fbc9b4d"), Name = "Music"},
            new MediaTypeEntity() {Id = new Guid("ee0c7412-c291-4adc-b503-02746c3a60d9"), Name = "Series"}
        );
    }
}