using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Application.DTOs;

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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null) {
            return NotFound();
            }

            return Ok(task);

        }


        [HttpPost]

        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            
            var createdTask = await _taskService.CreateTaskAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id,UpdateTaskDto dto)
        {
            

            var result = await _taskService.UpdateTaskAsync(id,dto);

            if (!result)
            {
                
                return NotFound();

            }

            return NoContent();
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
