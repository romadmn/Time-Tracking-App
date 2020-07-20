using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ITProjects.BLL.DataTransferObjects.TaskDto;
using ITProjects.DAL.Entities;

namespace ITProjects.BLL.MapperProfilers
{
    public class TaskProfiler : Profile
    {
        public TaskProfiler()
        {
            CreateMap<TaskGetDto, Task>().ReverseMap()
                .ForMember(a => a.Id, opt => opt.Condition(a => a.Id != 0))
                .ForMember(a => a.StartDate, opt => opt.MapFrom(x => x.TaskLists.Last().StartDate))
                .ForMember(a => a.CancelDate, opt => opt.MapFrom(x => x.TaskLists.Last().CancelDate))
                .AfterMap((entity, model) =>
            {
                if (model.StartDate != DateTime.MinValue)
                {
                    if (model.CancelDate != DateTime.MinValue)
                    {
                        model.TimeSpentOnTheTask = model.CancelDate.Date.Subtract(model.StartDate);
                    }
                    model.TimeSpentOnTheTask = DateTime.UtcNow.Subtract(model.StartDate);
                }
            });
            CreateMap<TaskPostDto, Task>().ReverseMap()
                .ForMember(a => a.Id, opt => opt.Condition(a => a.Id != 0));
            CreateMap<TaskPutDto, Task>().ReverseMap()
                .ForMember(a => a.Id, opt => opt.Condition(a => a.Id != 0));
        }
    }
}
