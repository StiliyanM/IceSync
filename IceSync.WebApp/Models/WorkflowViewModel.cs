namespace IceSync.WebApp.Models;

public class WorkflowViewModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsActive { get; set; }

    public bool IsRunning { get; set; }

    public string? MultiExecBehavior { get; set; }
}
