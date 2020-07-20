using System;
using System.Collections.Generic;
using System.Text;
using ITProjects.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITProjects.DAL.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Task");
            builder.Property(e => e.Id)
                .HasColumnName("Id");

            builder.Property(e => e.Ticket)
                .IsRequired()
                .HasColumnName("Ticket")
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(1000);

            builder.Property(a => a.CreateDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
