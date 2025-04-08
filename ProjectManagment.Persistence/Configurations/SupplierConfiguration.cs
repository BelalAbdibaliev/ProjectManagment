using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Persistence.Configurations;

public class SupplierConfiguration: IEntityTypeConfiguration<SupplierCompany>
{
    public void Configure(EntityTypeBuilder<SupplierCompany> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(25);
        
        builder.HasMany(p => p.Projects)
            .WithOne(s => s.Supplier)
            .HasForeignKey(s => s.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}