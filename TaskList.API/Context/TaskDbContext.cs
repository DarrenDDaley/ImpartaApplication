using System;
using System.Collections.Generic;
using System.Linq;
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

        private DbSet<TaskItem> tasks { get; set; }

        public void Add(TaskItem item) => base.Add(item);

        public void Remove(TaskItem item) => base.Remove(item);

        public void Update(TaskItem item) => base.Update(item);

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public Task<TaskItem> Get(Guid id) => tasks.SingleOrDefaultAsync(t => t.Id == id);

        public Task<bool> Exists(Guid id) => tasks.AnyAsync(t => t.Id == id);

        public Task<List<TaskItem>> GetAll() =>  tasks
                                                .OrderBy(t => t.TimeAdded)
                                                .ToListAsync();
    }
}
