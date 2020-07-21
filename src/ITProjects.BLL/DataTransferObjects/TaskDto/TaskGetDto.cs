using System;
using System.Collections.Generic;
using System.Text;

namespace ITProjects.BLL.DataTransferObjects.TaskDto
{
    public class TaskGetDto
    {
        public int Id { get; set; }
        public string Ticket { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CancelDate { get; set; }
        public TimeSpan TimeSpentOnTheTask { get; set; }
    }
}
