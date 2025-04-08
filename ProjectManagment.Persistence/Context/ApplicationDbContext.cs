using Microsoft.EntityFrameworkCore;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Persistence.Context;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<SupplierCompany> SupplierCompanies { get; set; }
    public DbSet<ClientCompany> ClientCompanies { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}