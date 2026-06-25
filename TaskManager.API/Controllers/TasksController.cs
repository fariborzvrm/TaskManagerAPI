using Microsoft.AspNetCore.Mvc;


namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TasksController:ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = new string[] { "Task 1", "Task 2", "Task 3" };
            return Ok(tasks); 
        }
    }
}
