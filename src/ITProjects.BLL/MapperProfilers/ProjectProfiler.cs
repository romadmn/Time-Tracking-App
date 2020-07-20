using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ITProjects.BLL.DataTransferObjects.ProjectDto;
using ITProjects.DAL.Entities;

namespace ITProjects.BLL.MapperProfilers
{
    public class ProjectProfiler : Profile
    {
        public ProjectProfiler()
        {
            CreateMap<ProjectGetDto, Project>().ReverseMap()
                .ForMember(a => a.Id, opt => opt.Condition(a => a.Id != 0));
            CreateMap<ProjectPostDto, Project>().ReverseMap()
                .ForMember(a => a.Id, opt => opt.Condition(a => a.Id != 0));
            CreateMap<ProjectPutDto, Project>().ReverseMap()
                .ForMember(a => a.Id, opt => opt.Condition(a => a.Id != 0));
        }
    }
}
