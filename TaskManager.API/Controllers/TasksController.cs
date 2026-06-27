using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.API.Interfaces;


namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TasksController : ControllerBase
    {

        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null) {
            return NotFound();
            }

            return Ok(task);
            
        }


        [HttpPost]

        public async Task<IActionResult> Create(TaskItem task)
        {
            var createdTask = await _taskService.CreateTaskAsync(task);

            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, TaskItem task)
        {
            var result = await _taskService.UpdateTaskAsync(id, task);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
