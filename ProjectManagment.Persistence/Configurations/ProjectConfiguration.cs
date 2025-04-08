using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Persistence.Configurations;

public class ProjectConfiguration: IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasOne(m => m.Manager)
            .WithMany(p => p.Projects)
            .HasForeignKey(m => m.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(c => c.Client)
            .WithMany(p => p.Projects)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(s => s.Supplier)
            .WithMany(p => p.Projects)
            .HasForeignKey(s => s.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}