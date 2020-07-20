using System;
using System.Collections.Generic;
using System.Text;

namespace ITProjects.DAL.Entities
{
    public class Task : IEntityBase
    {
        public int Id { get; set; }
        public string Ticket { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<TaskList> TaskLists { get; set; }
    }
}
