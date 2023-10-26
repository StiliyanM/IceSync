using MediatR;

namespace IceSync.Application.Queries.GetAllWorkflows
{
    public record GetAllWorkflowsQuery : IRequest<IEnumerable<GetAllWorkflowsQueryResult>>;
}
