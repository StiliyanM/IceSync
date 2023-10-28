using IceSync.Domain.Interfaces;
using MediatR;

namespace IceSync.Application.Commands.SyncWorkflows
{
    public class SyncWorkflowsCommandHandler : IRequestHandler<SyncWorkflowsCommand>
    {
        private readonly IWorkflowExternalService _workflowExternalService;
        private readonly IWorkflowRepository _workflowRepository;

        public SyncWorkflowsCommandHandler(
            IWorkflowExternalService workflowExternalService, IWorkflowRepository workflowRepository)
        {
            _workflowExternalService = workflowExternalService;
            _workflowRepository = workflowRepository;
        }

        public async Task Handle(SyncWorkflowsCommand request, CancellationToken cancellationToken)
        {
            var externalWorkflows = await _workflowExternalService.GetWorkflowsAsync(cancellationToken);
            var dbWorkflows = await _workflowRepository.GetAllAsync(cancellationToken);

            var externalWorkflowIds = new HashSet<int>(externalWorkflows.Select(w => w.Id));
            var dbWorkflowIds = new HashSet<int>(dbWorkflows.Select(w => w.Id));

            var workflowsToInsert = externalWorkflows.Where(w => !dbWorkflowIds.Contains(w.Id)).ToList();
            var workflowsToDelete = dbWorkflows.Where(w => !externalWorkflowIds.Contains(w.Id)).ToList();

            var workflowsToUpdate = externalWorkflows.Intersect(dbWorkflows).ToList();

            await _workflowRepository.InsertManyAsync(workflowsToInsert, cancellationToken);
            await _workflowRepository.DeleteManyAsync(workflowsToDelete, cancellationToken);
            await _workflowRepository.UpdateManyAsync(workflowsToUpdate, cancellationToken);

            await _workflowRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
