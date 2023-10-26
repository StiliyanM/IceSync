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
    {
        return await _context.Workflows.ToListAsync(cancellationToken);
    }

    public async Task InsertManyAsync(IEnumerable<Workflow> workflows, CancellationToken cancellationToken)
    {
        await _context.Workflows.AddRangeAsync(workflows, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateManyAsync(IEnumerable<Workflow> workflows, CancellationToken cancellationToken)
    {
        _context.Workflows.UpdateRange(workflows);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteManyAsync(IEnumerable<Workflow> workflows, CancellationToken cancellationToken)
    {
        _context.Workflows.RemoveRange(workflows);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
