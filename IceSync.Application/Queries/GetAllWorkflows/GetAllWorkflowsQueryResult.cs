﻿namespace IceSync.Application.Queries.GetAllWorkflows
{
    public class GetAllWorkflowsQueryResult
    {
        public int WorkflowId { get; set; }

        public string? WorkflowName { get; set; }

        public bool IsActive { get; set; }

        public bool IsRunning { get; set; }

        public string? MultiExecBehavior { get; set; }
    }
}
