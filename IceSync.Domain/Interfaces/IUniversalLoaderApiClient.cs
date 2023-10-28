using IceSync.Domain.Models;

namespace IceSync.Domain.Interfaces
{
    public interface IUniversalLoaderApiClient
    {
        Task<IEnumerable<Workflow>> GetWorkflowsAsync(CancellationToken cancellationToken);

        Task<bool> RunWorkflowAsync(int id, CancellationToken cancellationToken);
    }
}