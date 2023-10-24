﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IceSync.Infrastructure.Models;

namespace IceSync.Infrastructure.Configurations
{
    public class WorkflowConfiguration : IEntityTypeConfiguration<Workflow>
    {
        public void Configure(EntityTypeBuilder<Workflow> builder)
        {
            builder.HasKey(w => w.WorkflowId);

            builder.Property(w => w.WorkflowId)
                   .ValueGeneratedNever(); // Assuming the ID comes from an external system and is not auto-generated.
        }
    }
}