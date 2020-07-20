using System;
using System.Collections.Generic;
using System.Text;
using ITProjects.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITProjects.DAL.Configuration
{
    public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
    {
        public void Configure(EntityTypeBuilder<TaskList> builder)
        {
            builder.ToTable("TaskList");
            builder.Property(e => e.Id)
                .HasColumnName("Id");

            builder.Property(a => a.CreateDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
