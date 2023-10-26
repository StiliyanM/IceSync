using IceSync.Domain.Models;

namespace IceSync.Domain.Interfaces
{
    public interface IUniversalLoaderApiClient
    {
        Task<IEnumerable<Workflow>> GetWorkflowsAsync(CancellationToken cancellationToken);
    }
}