using MediatR;

namespace IceSync.Application.Commands.SyncWorkflows;

public record RunWorkflowCommand(int Id) : IRequest<bool>;
