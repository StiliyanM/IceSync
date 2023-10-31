using IceSync.Domain.Interfaces;
using IceSync.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IceSync.Infrastructure.Repositories;

public class WorkflowRepository : IWorkflowRepository
{
    private readonly WorkflowDbContext _context;

    public WorkflowRepository(WorkflowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Workflow>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Workflows.ToListAsync(cancellationToken);

    public async Task<IEnumerable<Workflow>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken)
        => await _context.Workflows.AsNoTracking().ToListAsync(cancellationToken);

    public async Task InsertManyAsync(
        IEnumerable<Workflow> workflows,
        CancellationToken cancellationToken,
        bool commitImmediately)
    {
        await _context.Workflows.AddRangeAsync(workflows, cancellationToken);
        if (commitImmediately)
        {
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateManyAsync(
        IEnumerable<Workflow> workflows,
        CancellationToken cancellationToken,
        bool commitImmediately)
    {
        _context.Workflows.UpdateRange(workflows);
        if (commitImmediately)
        {
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteManyAsync(
        IEnumerable<Workflow> workflows,
        CancellationToken cancellationToken,
        bool commitImmediately)
    {
        _context.Workflows.RemoveRange(workflows);
        if (commitImmediately)
        {
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);
}