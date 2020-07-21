using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ITProjects.BLL.DataTransferObjects.TaskDto;
using ITProjects.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITProjects.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<List<TaskGetDto>>> GetAllAsync([FromQuery] int projectId, [FromQuery] string? dateCreated)
        {
            var tasks = dateCreated == null
                ? await _taskService.GetAllByDateAsync(projectId, DateTime.UtcNow)
                : await _taskService.GetAllByDateAsync(projectId, DateTime.ParseExact(dateCreated, "yyyyMMdd",
                    CultureInfo.InvariantCulture));
            return Ok(tasks);
        }


        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskGetDto>> PostAsync([FromBody] TaskPostDto taskDto)
        {
            var insertedTask = await _taskService.AddAsync(taskDto);
            return Ok(insertedTask);
        }

        // POST: api/tasks
        [HttpPost("{id}/start")]
        public async Task<ActionResult> StartAsync([FromRoute] int id)
        {
            var isStartedTask = await _taskService.StartAsync(id);
            if (!isStartedTask)
            {
                return NotFound();
            }
            return Ok();
        }

        // POST: api/tasks
        [HttpPut("{id}/cancel")]
        public async Task<ActionResult> CancelAsync([FromRoute] int id)
        {
            var isCanceledTask = await _taskService.CancelAsync(id);
            if (!isCanceledTask)
            {
                return NotFound();
            }
            return Ok();
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] TaskPutDto taskDto)
        {
            if (id != taskDto.Id)
            {
                return BadRequest();
            }

            if (taskDto == null)
            {
                return BadRequest();
            }

            var isTaskUpdated = await _taskService.UpdateAsync(taskDto);
            if (!isTaskUpdated)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            var isTaskRemoved = await _taskService.RemoveAsync(id);
            if (!isTaskRemoved)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
