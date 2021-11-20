using FraudDetector.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FraudDetector.Infrastructure.Database.Configurations;

public class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(person => person.Id);

        builder.Property(person => person.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(person => person.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(person => person.IdentificationNumber)
            .HasMaxLength(255);

        builder.Property(person => person.DateOfBirth);
        builder.Property(person => person.Created);
        builder.Property(person => person.LastModified);
    }
}