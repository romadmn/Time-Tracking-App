using System;
using System.Collections.Generic;
using System.Text;
using ITProjects.DAL.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace ITProjects.DAL
{
    public class DataInitializer
    {
        public static void Initialize(ITProjectsContext context)
        {
            if (!context.Project.Any())
            {
                context.Add(new Project()
                {
                    Name = "ITProjects",
                });

                context.SaveChanges();
            }
            if (!context.Task.Any())
            {
                context.Add(new Task()
                {
                    Ticket = "Do test task",
                    Description = "Just do it!",
                    ProjectId = 1
                });

                context.SaveChanges();
            }
        }
    }
}
