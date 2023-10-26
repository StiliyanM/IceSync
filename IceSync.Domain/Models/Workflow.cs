namespace IceSync.Domain.Models
{
    public class Workflow
    {
        public int WorkflowId { get; set; }

        public required string WorkflowName { get; set; }

        public bool IsActive { get; set; }

        public bool IsRunning { get; set; }

        public required string MultiExecBehavior { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Workflow otherWorkflow)
            {
                return WorkflowId == otherWorkflow.WorkflowId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return WorkflowId.GetHashCode();
        }
    }
}
