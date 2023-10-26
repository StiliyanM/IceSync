using IceSync.Domain.Interfaces;
using MediatR;

namespace IceSync.Application.Queries.GetAllWorkflows
{
    public class GetAllWorkflowsQueryHandler :
        IRequestHandler<GetAllWorkflowsQuery, IEnumerable<GetAllWorkflowsQueryResult>>
    {
        private readonly IWorkflowRepository _workflowRepository;

        public GetAllWorkflowsQueryHandler(IWorkflowRepository workflowRepository)
        {
            _workflowRepository = workflowRepository;
        }

        public async Task<IEnumerable<GetAllWorkflowsQueryResult>> Handle(
            GetAllWorkflowsQuery request, CancellationToken cancellationToken)
        {
            var workflows = await _workflowRepository.GetAllAsync(cancellationToken);

            return workflows.Select(w => new GetAllWorkflowsQueryResult
            {
                WorkflowId = w.WorkflowId,
                WorkflowName = w.WorkflowName,
                MultiExecBehavior = w.MultiExecBehavior,
                IsActive = w.IsActive,
                IsRunning = w.IsRunning,
            });
        }
    }
}
