using System;
using System.Collections.Generic;
using System.Text;

namespace ITProjects.DAL.Entities
{
    public class TaskList : IEntityBase
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
