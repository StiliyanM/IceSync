namespace IceSync.Domain.Models
{
    public class Workflow
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsRunning { get; set; }

        public string? MultiExecBehavior { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Workflow otherWorkflow)
            {
                return Id == otherWorkflow.Id &&
                       Name == otherWorkflow.Name &&
                       IsActive == otherWorkflow.IsActive &&
                       IsRunning == otherWorkflow.IsRunning &&
                       MultiExecBehavior == otherWorkflow.MultiExecBehavior;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
