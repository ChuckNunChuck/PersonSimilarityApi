using FraudDetector.Domain.Common;
using FraudDetector.Domain.Model;
using FraudDetector.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FraudDetector.Infrastructure.Database;

public class FraudDetectorContext : DbContext
{
    public FraudDetectorContext(DbContextOptions<FraudDetectorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> Persons => Set<Person>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
    }
}