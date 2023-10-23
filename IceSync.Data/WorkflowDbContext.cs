using IceSync.Data.Configurations;
using IceSync.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IceSync.Data
{
    public class WorkflowDbContext : DbContext
    {
        public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options) : base(options) { }

        public DbSet<Workflow> Workflows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkflowConfiguration());
        }

    }
}
