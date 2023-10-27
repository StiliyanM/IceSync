namespace IceSync.Domain.Models
{
    public class Workflow
    {
        public int WorkflowId { get; set; }

        public string? WorkflowName { get; set; }

        public bool IsActive { get; set; }

        public bool IsRunning { get; set; }

        public string? MultiExecBehavior { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Workflow otherWorkflow)
            {
                return WorkflowId == otherWorkflow.WorkflowId &&
                       WorkflowName == otherWorkflow.WorkflowName &&
                       IsActive == otherWorkflow.IsActive &&
                       IsRunning == otherWorkflow.IsRunning &&
                       MultiExecBehavior == otherWorkflow.MultiExecBehavior;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return WorkflowId.GetHashCode();
        }
    }
}
