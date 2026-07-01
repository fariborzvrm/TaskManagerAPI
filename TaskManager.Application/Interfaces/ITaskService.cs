using TaskManager.Application.DTOs;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskResponseDto>> GetAllTasksAsync();
        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto);
        Task<TaskResponseDto> GetByIdAsync(int id);
        Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto);
        Task<bool> DeleteTaskAsync(int id);
    }
}
