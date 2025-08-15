using AuroraRates.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuroraRates.DataAccess;

public class AuroraRatesDatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<MediaTypeEntity> MediaTypes { get; set; }
    public AuroraRatesDatabaseContext(DbContextOptions<AuroraRatesDatabaseContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServerConnectionString"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuroraRatesDatabaseContext).Assembly);
    }
}
