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
            .WithMany(e => e.Employees)
            .UsingEntity(j => j.ToTable("ProjectEmployees"));

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
        
        builder.HasMany(e => e.Tasks)
            .WithOne(e => e.Manager)
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(m => m.AuthoredTasks)
            .WithOne(e => e.Author)
            .HasForeignKey(e => e.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}