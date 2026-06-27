using TaskManager.API.Models;

namespace TaskManager.API.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<TaskItem> GetByIdAsync(int id);
        Task<bool> UpdateTaskAsync(int id, TaskItem task);
        Task<bool> DeleteTaskAsync(int id);
    }
}
