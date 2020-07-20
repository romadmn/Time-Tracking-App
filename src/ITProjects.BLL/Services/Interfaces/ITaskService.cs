using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITProjects.BLL.DataTransferObjects.TaskDto;

namespace ITProjects.BLL.Services.Interfaces
{
    public interface ITaskService
    {

        /// <summary>
        /// Retrieve all tasks
        /// </summary>
        /// <param name="projectId">project`s id</param>
        /// <param name="dateCreated">task`s date</param>
        /// <returns>returns list of project DTOs</returns>
        Task<List<TaskGetDto>> GetAllByDateAsync(int projectId, DateTime? dateCreated);

        /// <summary>
        /// Update specified task
        /// </summary>
        /// <param name="task">task DTO instance</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TaskPutDto task);

        /// <summary>
        /// Remove task from database
        /// </summary>
        /// <param name="taskId">task's ID</param>
        /// <returns></returns>
        Task<bool> RemoveAsync(int taskId);

        /// <summary>
        /// Create new task and add it into Database
        /// </summary>
        /// <param name="task">task DTO instance</param>
        /// <returns>Returns inserted task instance</returns>
        Task<TaskGetDto> AddAsync(TaskPostDto task);

        /// <summary>
        /// Start task
        /// </summary>
        /// <param name="taskId">task`s id</param>
        /// <returns></returns>
        Task<bool> StartAsync(int taskId);

        /// <summary>
        /// Cancel task
        /// </summary>
        /// <param name="taskId">task`s id</param>
        /// <returns></returns>
        Task<bool> CancelAsync(int taskId);

    }
}
