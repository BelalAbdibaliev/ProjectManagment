using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.HasMany(p => p.Projects)
            .WithOne(e => e.Manager)
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(e => e.MiddleName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(60);
    }
}