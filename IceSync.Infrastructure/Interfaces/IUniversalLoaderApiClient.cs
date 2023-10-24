using IceSync.Infrastructure.Models;

namespace IceSync.Infrastructure.Interfaces
{
    public interface IUniversalLoaderApiClient
    {
        Task<IEnumerable<Workflow>> GetWorkflowsAsync(CancellationToken cancellationToken);
    }
}