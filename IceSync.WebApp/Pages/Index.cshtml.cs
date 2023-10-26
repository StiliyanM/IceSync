using IceSync.Application.Queries.GetAllWorkflows;
using IceSync.Infrastructure.Settings;
using IceSync.WebApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace IceSync.WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly IOptionsMonitor<UniversalLoaderApiSettings> _universalApiSettingsMonitor;

    public IndexModel(IMediator mediator, IOptionsMonitor<UniversalLoaderApiSettings> universalApiSettingsMonitor)
    {
        _mediator = mediator;
        _universalApiSettingsMonitor = universalApiSettingsMonitor;
    }

    public IEnumerable<WorkflowViewModel> Workflows { get; set; } = new List<WorkflowViewModel>();

    public string GetRunWorkflowUrl(int workflowId) => string.Format(
        _universalApiSettingsMonitor.CurrentValue.RunWorkflowEndpoint,
        workflowId);

    public async Task OnGetAsync()
    {
        var workflows = await _mediator.Send(new GetAllWorkflowsQuery());
        Workflows = workflows.Select(w => new WorkflowViewModel
        {
            WorkflowId = w.WorkflowId,
            WorkflowName = w.WorkflowName,
            MultiExecBehavior = w.MultiExecBehavior,
            IsRunning = w.IsRunning,
            IsActive = w.IsActive  
        });
    }
}
