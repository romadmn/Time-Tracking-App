using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITProjects.BLL.DataTransferObjects.ProjectDto;

namespace ITProjects.BLL.Services.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Retrieve project by ID
        /// </summary>
        /// <param name="projectId">project's ID</param>
        /// <returns>returns project DTO</returns>
        Task<ProjectGetDto> GetByIdAsync(int projectId);

        /// <summary>
        /// Retrieve all projects
        /// </summary>
        /// <returns>returns list of project DTOs</returns>
        Task<List<ProjectGetDto>> GetAllAsync();

        /// <summary>
        /// Update specified project
        /// </summary>
        /// <param name="project">project DTO instance</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(ProjectPutDto project);

        /// <summary>
        /// Remove project from database
        /// </summary>
        /// <param name="projectId">project's ID</param>
        /// <returns></returns>
        Task<bool> RemoveAsync(int projectId);

        /// <summary>
        /// Create new project and add it into Database
        /// </summary>
        /// <param name="project">project DTO instance</param>
        /// <returns>Returns inserted project instance</returns>
        Task<ProjectGetDto> AddAsync(ProjectPostDto project);

    }
}
