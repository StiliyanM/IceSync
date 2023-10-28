using IceSync.Application.Commands.SyncWorkflows;
using IceSync.Application.Queries.GetAllWorkflows;
using IceSync.WebApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IceSync.WebApp.Pages;

[IgnoreAntiforgeryToken]
public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IEnumerable<WorkflowViewModel> Workflows { get; set; } = new List<WorkflowViewModel>();

    public string GetRunWorkflowUrl(int id)
        => $"/Index?handler=Run&id={id}";

    public async Task OnGetAsync()
    {
        var workflows = await _mediator.Send(new GetAllWorkflowsQuery());
        Workflows = workflows.Select(w => new WorkflowViewModel
        {
            Id = w.Id,
            Name = w.Name,
            MultiExecBehavior = w.MultiExecBehavior,
            IsRunning = w.IsRunning,
            IsActive = w.IsActive
        });
    }

    public async Task<IActionResult> OnPostRunAsync(int id)
    {
        var result = await _mediator.Send(new RunWorkflowCommand(id));

        return new JsonResult(new { success = result });
    }
}
