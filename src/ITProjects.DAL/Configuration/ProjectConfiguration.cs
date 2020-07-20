using System;
using System.Collections.Generic;
using System.Text;
using ITProjects.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITProjects.DAL.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project");
            builder.Property(e => e.Id)
                .HasColumnName("Id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(50);

            builder.Property(a => a.CreateDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
