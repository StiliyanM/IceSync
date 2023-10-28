using IceSync.Application.Commands.SyncWorkflows;
using IceSync.Domain.Interfaces;
using MediatR;

namespace IceSync.Application.Commands.RunWorkflow
{
    public class RunWorkflowCommandHandler : IRequestHandler<RunWorkflowCommand, bool>
    {
        private readonly IWorkflowExternalService _workflowExternalService;

        public RunWorkflowCommandHandler(IWorkflowExternalService workflowExternalService)
        {
            _workflowExternalService = workflowExternalService;
        }

        public async Task<bool> Handle(RunWorkflowCommand request, CancellationToken cancellationToken)
            => await _workflowExternalService.RunWorkflowAsync(request.Id, cancellationToken);
    }
}
