using System;
using System.Collections.Generic;
using System.Text;
using ITProjects.BLL.DataTransferObjects.TaskDto;

namespace ITProjects.BLL.DataTransferObjects.ProjectDto
{
    public class ProjectGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskGetDto> Tasks { get; set; }
        public TimeSpan TotalTimeSpentOnTasks { get; set; }
    }
}
