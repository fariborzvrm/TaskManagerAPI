using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models;

namespace TaskManager.API
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TaskApiDb;Trusted_Connection=True;");
        }

        public DbSet<Models.TaskItem> TaskItems { get; set; }
    }
}
