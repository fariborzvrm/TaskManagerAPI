using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MinLength (3)]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
