using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskList.API.Database;
using TaskList.API.Models;

namespace TaskList.Tests
{
    public class TaskDbContextStub : ITaskDbContext<TaskItem>
    {

        private List<TaskItem> items = new List<TaskItem>();

        public DbSet<TaskItem> Tasks { get => (DbSet<TaskItem>)items.AsQueryable();
                                       set => items =(List<TaskItem>) value.AsQueryable(); }

        public void Add(TaskItem item) => items.Add(item);

        public void Remove(TaskItem item) => items.Remove(item);

        public Task<int> SaveChangesAsync() => Task.FromResult(1);

        public void Update(TaskItem item) { }

        public void Dispose() => throw new NotImplementedException();
    }
}
