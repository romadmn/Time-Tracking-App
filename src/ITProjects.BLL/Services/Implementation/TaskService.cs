using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ITProjects.BLL.DataTransferObjects.TaskDto;
using ITProjects.BLL.Services.Interfaces;
using ITProjects.DAL.Entities;
using ITProjects.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Task = ITProjects.DAL.Entities.Task;

namespace ITProjects.BLL.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<DAL.Entities.Task> _taskRepository;
        private readonly IRepository<DAL.Entities.TaskList> _taskListRepository;
        private readonly IMapper _mapper;

        public TaskService(IRepository<DAL.Entities.Task> taskRepository, IRepository<DAL.Entities.TaskList> taskListRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _taskListRepository = taskListRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<TaskGetDto>> GetAllByDateAsync(int projectId, DateTime? dateCreated)
        {
            var tasks = dateCreated != null
                ? await _taskRepository.GetAll().Include(x=>x.TaskLists).Where(x => x.ProjectId == projectId && x.CreateDate.Day == dateCreated.Value.Day).ToListAsync()
                : await _taskRepository.GetAll().Include(x => x.TaskLists).Where(x => x.ProjectId == projectId && x.CreateDate.Day == DateTime.UtcNow.Day).ToListAsync();

            if (tasks == null)
            {
                return null;
            }
            return _mapper.Map<List<TaskGetDto>>(tasks);
        }


        /// <inheritdoc />
        public async Task<TaskGetDto> AddAsync(TaskPostDto taskDto)
        {
            var task = _mapper.Map<Task>(taskDto);
            _taskRepository.Add(task);
            await _taskRepository.SaveChangesAsync();
            return _mapper.Map<TaskGetDto>(task);
        }

        /// <inheritdoc />
        public async Task<bool> StartAsync(int taskId)
        {
            var task = await _taskRepository.FindByCondition(x => x.Id == taskId);
            var taskList = new TaskList()
            {
                CreateDate = task.CreateDate,
                StartDate = DateTime.UtcNow,
                Task = task
            };
            _taskListRepository.Add(taskList);
            var started = await _taskListRepository.SaveChangesAsync();
            return started > 0;
        }

        /// <inheritdoc />
        public async Task<bool> CancelAsync(int taskId)
        {
            var taskList = _taskListRepository.GetAll().Where(x => x.TaskId == taskId).ToList().LastOrDefault();
            if (taskList == null)
            {
                return false;
            }
            taskList.CancelDate = DateTime.UtcNow;
            _taskListRepository.Update(taskList);
            var canceled = await _taskListRepository.SaveChangesAsync();
            return canceled > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveAsync(int taskId)
        {
            var task = await _taskRepository.FindByCondition(x => x.Id == taskId);
            if (task == null)
            {
                return false;
            }
            _taskRepository.Remove(task);
            var affectedRows = await _taskRepository.SaveChangesAsync();
            return affectedRows > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(TaskPutDto taskDto)
        {
            var task = _mapper.Map<Task>(taskDto);
            _taskRepository.Update(task);
            var affectedRows = await _taskRepository.SaveChangesAsync();
            return affectedRows > 0;
        }

    }
}
