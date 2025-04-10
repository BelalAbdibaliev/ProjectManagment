using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Persistence.Configurations;

public class ProjectTaskConfiguration: IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.HasOne(pt => pt.Project)
            .WithMany(pt => pt.Tasks)
            .HasForeignKey(pt => pt.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(p => p.Author)
            .WithMany(pt => pt.AuthoredTasks)
            .HasForeignKey(pt => pt.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(t => t.Manager)
            .WithMany(pt => pt.Tasks)
            .HasForeignKey(pt => pt.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}