using MediatR;

namespace IceSync.Application.Commands;

public record SyncWorkflowsCommand : IRequest<Unit>;
