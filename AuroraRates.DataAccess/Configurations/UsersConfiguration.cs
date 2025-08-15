using AuroraRates.DataAccess.Entity;
using AuroraRates.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuroraRates.DataAccess.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.HasMany(u => u.Reviews).WithOne(r => r.User);
        builder.HasIndex(u => u.Nickname).IsUnique();
    }
}