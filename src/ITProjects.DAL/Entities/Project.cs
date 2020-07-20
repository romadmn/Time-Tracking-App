using System;
using System.Collections.Generic;
using System.Text;

namespace ITProjects.DAL.Entities
{
    public class Project : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
