using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Mappings
{
    public static class TaskMappings
    {
        public static TaskResponseDto ToTaskResponseDto (this TaskItem task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                IsCompleted = task.IsCompleted,
            };
        }

    }
}
