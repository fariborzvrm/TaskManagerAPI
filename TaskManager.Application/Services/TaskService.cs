
using System.ComponentModel.DataAnnotations;
using TaskManager.Application.DTOs;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Mappings;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TaskResponseDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();            

            return tasks.Select(task => task.ToTaskResponseDto()).ToList();
            
        }

        public async Task<TaskResponseDto> GetByIdAsync(int id)
        {

            var task= await _taskRepository.GetByIdAsync(id);

            return task == null ?
                throw new NotFoundException("Task not found.") : 
                task.ToTaskResponseDto();

            
        }


        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto)
        {
            var duplicate = await _taskRepository.TaskExistsByTitleAsync(dto.Title);

            if (duplicate)
            
                throw new DuplicateTaskTitleException($"A task with the title '{dto.Title}' already exists.");
            

            var task = new TaskItem
            {
                Title = dto.Title,
            };

            await _taskRepository.CreateTaskAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return task.ToTaskResponseDto();

        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            
           var task = await _taskRepository.GetByIdAsync(id); 
           
           if (task==null)
            
                throw new NotFoundException("Task not found.");

           var isDuplicateTitle = await _taskRepository.ExistsByTitleExceptIdAsync(dto.Title, id);

            if (isDuplicateTitle)
            
                throw new DuplicateTaskTitleException($"A task with the title '{dto.Title}' already exists.");
            

            task.Title = dto.Title;
           task.IsCompleted = dto.IsCompleted;

            await _unitOfWork.SaveChangesAsync();
            return true;


        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.DeleteTaskAsync(id);

            if (!task) 
            return false;
            

            return true;

        }
    }
}
