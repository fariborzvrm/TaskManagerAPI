using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.DTOs
{
    public class UpdateTaskDto
    {        
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
