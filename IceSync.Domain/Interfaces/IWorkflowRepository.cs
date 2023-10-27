using IceSync.Domain.Models;

namespace IceSync.Domain.Interfaces;

public interface IWorkflowRepository
{
    Task DeleteManyAsync(
        IEnumerable<Workflow> workflows, 
        CancellationToken cancellationToken, 
        bool commitImmediately = false);

    Task<IEnumerable<Workflow>> GetAllAsync(CancellationToken cancellationToken);

    Task InsertManyAsync(
        IEnumerable<Workflow> workflows, 
        CancellationToken cancellationToken, 
        bool commitImmediately = false);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task UpdateManyAsync(
        IEnumerable<Workflow> workflows, 
        CancellationToken cancellationToken, 
        bool commitImmediately = false);
}
