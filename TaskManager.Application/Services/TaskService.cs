
using AutoMapper;
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
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IUnitOfWork unitOfWork , IMapper mapper)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }

        public async Task<List<TaskResponseDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();            

           
           return _mapper.Map<List<TaskResponseDto>>(tasks);

        }

        public async Task<TaskResponseDto> GetByIdAsync(int id)
        {

            var task= await _taskRepository.GetByIdAsync(id);

            return task == null ?
                throw new NotFoundException("Task not found.") : 
                
                _mapper.Map<TaskResponseDto>(task);

            
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

            

            return _mapper.Map<TaskResponseDto>(task);

        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            
           var task = await _taskRepository.GetByIdAsync(id); 
           
           if (task==null)
            
                throw new NotFoundException("Task not found.");

           var isDuplicateTitle = await _taskRepository.ExistsByTitleExceptIdAsync(dto.Title, id);

            if (isDuplicateTitle)
            
                throw new DuplicateTaskTitleException($"A task with the title '{dto.Title}' and a different '{id}' already exists.");
            

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
