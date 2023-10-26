using IceSync.Domain.Models;
using IceSync.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IceSync.Infrastructure;

public class WorkflowDbContext : DbContext
{
    public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options) : base(options) { }

    public DbSet<Workflow> Workflows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WorkflowConfiguration());
    }
}
