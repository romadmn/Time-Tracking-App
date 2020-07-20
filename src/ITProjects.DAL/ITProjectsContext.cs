using System;
using System.Collections.Generic;
using System.Text;
using ITProjects.DAL.Configuration;
using ITProjects.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITProjects.DAL
{
    public class ITProjectsContext : DbContext
    {
        public ITProjectsContext(DbContextOptions<ITProjectsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Project> Project { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<TaskList> TaskList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new TaskListConfiguration());
        }
    }
}
