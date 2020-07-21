using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ITProjects.BLL.DataTransferObjects.ProjectDto;
using ITProjects.BLL.DataTransferObjects.TaskDto;
using ITProjects.BLL.Services.Interfaces;
using ITProjects.DAL.Entities;
using ITProjects.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace ITProjects.BLL.Services.Implementation
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<DAL.Entities.Task> _taskRepository;
        private readonly IMapper _mapper;

        public ProjectService(IRepository<Project> projectRepository, IRepository<DAL.Entities.Task> taskRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<ProjectGetDto>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAll().ToListAsync();

            if (projects == null)
            {
                return null;
            }
            return _mapper.Map<List<ProjectGetDto>>(projects);
        }

        /// <inheritdoc />
        public async Task<ProjectGetDto> GetByIdAsync(int projectId)
        {
            var project = _mapper.Map<ProjectGetDto>(
                await _projectRepository.FindByCondition(x => x.Id == projectId));
            project.Tasks = _mapper.Map<List<TaskGetDto>>(_taskRepository.GetAll().Include(x=>x.TaskLists).Where(x => x.CreateDate.Day == DateTime.Today.Day && x.ProjectId == projectId).ToList());
            project.TotalTimeSpentOnTasks = TimeSpan.Zero;
            foreach (var task in project.Tasks)
            {
                project.TotalTimeSpentOnTasks = project.TotalTimeSpentOnTasks.Add(task.TimeSpentOnTheTask);
            }
            return project;
        }

        /// <inheritdoc />
        public async Task<ProjectGetDto> AddAsync(ProjectPostDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            _projectRepository.Add(project);
            await _projectRepository.SaveChangesAsync();
            return _mapper.Map<ProjectGetDto>(project);
        }

        /// <inheritdoc />
        public async Task<bool> RemoveAsync(int projectId)
        {
            var project = await _projectRepository.FindByCondition(x => x.Id == projectId);
            if (project == null)
            {
                return false;
            }
            _projectRepository.Remove(project);
            var affectedRows = await _projectRepository.SaveChangesAsync();
            return affectedRows > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(ProjectPutDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            _projectRepository.Update(project);
            var affectedRows = await _projectRepository.SaveChangesAsync();
            return affectedRows > 0;
        }

    }
}
