using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<TaskItem?> GetByIdAsync(int id);
        Task<bool> TaskExistsByTitleAsync(string title);
        Task<bool> UpdateTaskAsync(int id, TaskItem task);
        Task<bool> DeleteTaskAsync(int id);
        Task<bool> ExistsByTitleExceptIdAsync(string title, int id);
        

    }
}
