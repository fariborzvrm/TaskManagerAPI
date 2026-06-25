using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models;


namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TasksController:ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context) {
        _context = context;
        }


        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _context.TaskItems.ToListAsync();
            return Ok(tasks); 
        }

        [HttpGet("count")]

        public async Task<IActionResult> GetCount()
        {
            var task = await _context.TaskItems.CountAsync();                       
            return Ok(task);
        }

        [HttpPost("addTask")]

        public async Task<IActionResult> Create(TaskItem task)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpPut("updateTask")]

        public async Task<IActionResult> Update(TaskItem task)
        {
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }
    }
}
