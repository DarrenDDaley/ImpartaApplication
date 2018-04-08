using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskList.API.Database;
using TaskList.API.Models;

namespace TaskList.Tests
{
    public class TaskDbContextStub : ITaskDbContext<TaskItem>
    {
        private List<TaskItem> tasks = new List<TaskItem>();

        public void Add(TaskItem item) => tasks.Add(item);

        public Task<bool> Exists(Guid id) => Task.FromResult(tasks.Any(t => t.Id == id));

        public Task<TaskItem> Get(Guid id) => Task.FromResult(tasks.SingleOrDefault(t => t.Id == id));

        public Task<List<TaskItem>> GetAll() => Task.FromResult(tasks);

        public void Remove(TaskItem item) => Task.FromResult(tasks.Remove(item));

        public Task<int> SaveChangesAsync() => Task.FromResult(1);

        public void Update(TaskItem item) { }

        public void Dispose() { }
    }
}
