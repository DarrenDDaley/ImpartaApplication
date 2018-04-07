using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskList.API.Models;

namespace TaskList.API.Database
{
    public class TaskDbContext : DbContext, ITaskDbContext<TaskItem>
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        { }

        public DbSet<TaskItem> Tasks { get; set; }

        public void Add(TaskItem item) => base.Add(item);

        public void Remove(TaskItem item) => base.Remove(item);

        public void Update(TaskItem item) => base.Update(item);

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}
