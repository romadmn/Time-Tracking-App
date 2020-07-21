using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITProjects.BLL.DataTransferObjects.ProjectDto;
using ITProjects.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITProjects.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/projects
        [HttpGet]
        public async Task<ActionResult<List<ProjectGetDto>>> GetAllAsync()
        {
            return Ok(await _projectService.GetAllAsync());
        }

        // GET: api/projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectGetDto>> GetByIdAsync([FromRoute] int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();
            return Ok(project);
        }

        // POST: api/projects
        [HttpPost]
        public async Task<ActionResult<ProjectPostDto>> PostAsync([FromBody] ProjectPostDto projectDto)
        {
            var insertedProject = await _projectService.AddAsync(projectDto);
            return CreatedAtAction("GetByIdAsync", new { id = insertedProject.Id }, insertedProject);
        }

        // PUT: api/projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] ProjectPutDto projectDto)
        {
            if (id != projectDto.Id)
            {
                return BadRequest();
            }

            if (projectDto == null)
            {
                return BadRequest();
            }

            var isProjectUpdated = await _projectService.UpdateAsync(projectDto);
            if (!isProjectUpdated)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/projects/5
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            var isProjectRemoved = await _projectService.RemoveAsync(id);
            if (!isProjectRemoved)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
