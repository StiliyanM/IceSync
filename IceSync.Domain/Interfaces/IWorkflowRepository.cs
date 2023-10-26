using IceSync.Domain.Models;

namespace IceSync.Domain.Interfaces;

public interface IWorkflowRepository
{
    Task<IEnumerable<Workflow>> GetAllAsync(CancellationToken cancellationToken);

    Task InsertManyAsync(IEnumerable<Workflow> workflows, CancellationToken cancellationToken);

    Task UpdateManyAsync(IEnumerable<Workflow> workflows, CancellationToken cancellationToken);

    Task DeleteManyAsync(IEnumerable<Workflow> workflows, CancellationToken cancellationToken);
}
