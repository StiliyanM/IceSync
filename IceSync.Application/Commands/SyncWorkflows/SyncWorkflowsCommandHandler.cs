using IceSync.Domain.Interfaces;
using MediatR;

namespace IceSync.Application.Commands.SyncWorkflows
{
    public class SyncWorkflowsCommandHandler : IRequestHandler<SyncWorkflowsCommand, Unit>
    {
        private readonly IUniversalLoaderApiClient _apiClient;
        private readonly IWorkflowRepository _workflowRepository;

        public SyncWorkflowsCommandHandler(
            IUniversalLoaderApiClient apiClient, IWorkflowRepository workflowRepository)
        {
            _apiClient = apiClient;
            _workflowRepository = workflowRepository;
        }

        public async Task<Unit> Handle(SyncWorkflowsCommand request, CancellationToken cancellationToken)
        {
            var apiWorkflows = await _apiClient.GetWorkflowsAsync(cancellationToken);
            var dbWorkflows = await _workflowRepository.GetAllAsync(cancellationToken);

            var workflowsToInsert = apiWorkflows.Except(dbWorkflows).ToList();
            var workflowsToDelete = dbWorkflows.Except(apiWorkflows).ToList();
            var workflowsToUpdate = apiWorkflows.Intersect(dbWorkflows).ToList();

            await _workflowRepository.InsertManyAsync(workflowsToInsert, cancellationToken);
            await _workflowRepository.DeleteManyAsync(workflowsToDelete, cancellationToken);
            await _workflowRepository.UpdateManyAsync(workflowsToUpdate, cancellationToken);


            return Unit.Value;
        }
    }
}
