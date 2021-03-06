﻿using ITProjects.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITProjects.BLL.DataTransferObjects.TaskDto
{
    public class TaskPostDto
    {
        public int Id { get; set; }
        public string Ticket { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
    }
}
