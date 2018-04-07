using System;
using System.Threading.Tasks;
using TaskList.API.Models;
using Microsoft.EntityFrameworkCore;


namespace TaskList.API.Database
{
    public interface ITaskDbContext<TEntity> : IDisposable
    {
        void Add(TEntity item);
        void Remove(TEntity item);
        void Update(TEntity item);
        Task<int> SaveChangesAsync();
        DbSet<TaskItem> Tasks { get; set; }
    }
}
