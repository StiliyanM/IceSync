using MediatR;

namespace IceSync.Application.Commands.SyncWorkflows;

public record SyncWorkflowsCommand : IRequest<Unit>;
