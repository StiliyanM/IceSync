namespace IceSync.WebApp.Models;

public class WorkflowViewModel
{
    public int WorkflowId { get; set; }

    public required string WorkflowName { get; set; }

    public bool IsActive { get; set; }

    public bool IsRunning { get; set; }

    public required string MultiExecBehavior { get; set; }
}
