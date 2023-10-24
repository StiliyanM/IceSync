using IceSync.Application.DTOs;

namespace IceSync.Application.Interfaces
{
    public interface IWorkflowService
    {
        Task<IEnumerable<WorkflowDto>> GetAllWorkflowsAsync(CancellationToken cancellationToken);

        Task<bool> RunWorkflowAsync(int workflowId);
    }
}
