
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllTasksAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }


        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
         return await _taskRepository.CreateTaskAsync(task);

        }

        public async Task<bool> UpdateTaskAsync(int id, TaskItem task)
        {
            var existingTask = await _taskRepository.UpdateTaskAsync(id, task);
           if (existingTask)
            {
                return true;
            }
           return false;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.DeleteTaskAsync(id);

            if (task) {
            return true;
            }

            return false;

        }
    }
}
