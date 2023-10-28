using IceSync.Application.Commands.SyncWorkflows;
using IceSync.Domain.Interfaces;
using MediatR;

namespace IceSync.Application.Commands.RunWorkflow
{
    public class RunWorkflowCommandHandler : IRequestHandler<RunWorkflowCommand, bool>
    {
        private readonly IUniversalLoaderApiClient _apiClient;

        public RunWorkflowCommandHandler(IUniversalLoaderApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<bool> Handle(RunWorkflowCommand request, CancellationToken cancellationToken)
            => await _apiClient.RunWorkflowAsync(request.Id, cancellationToken);
    }
}
