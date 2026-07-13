using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context) { 
        _context = context;
        }

        
        public async Task<List<TaskItem>> GetAllTasksAsync() =>

             await _context.TaskItems
            .AsNoTracking()
            .ToListAsync();        
        

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            _context.TaskItems.Add(task);
            
            return task;
            
        }

        public async Task<TaskItem?> GetByIdAsync(int id) =>
            await _context.TaskItems.FindAsync(id);


        public async Task<bool> TaskExistsByTitleAsync(string title) =>
        
            await _context.TaskItems
                .AnyAsync(task => task.Title.ToLower() == title.ToLower());

            
        

        public async Task<bool> UpdateTaskAsync(int id, TaskItem task)
        {
            var existingTask = await _context.TaskItems.FindAsync(id);
            if (existingTask == null)
            {
                return false;
            }
            existingTask.Title = task.Title;
            existingTask.IsCompleted = task.IsCompleted;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            
            if (task == null)
            {
                return false;
            }
            _context.TaskItems.Remove(task);
            

            return true;
        }

        public async Task<bool> ExistsByTitleExceptIdAsync( string title, int id) =>
        
                 await _context.TaskItems
                .AnyAsync(task => task.Title.ToLower() == title.ToLower() && task.Id != id);
        

        

    }
}
