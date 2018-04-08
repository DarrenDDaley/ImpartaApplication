using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TaskList.API.Database
{
    public interface ITaskDbContext<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> Get(Guid id);
        Task<bool> Exists(Guid id);
        void Add(TEntity item);
        void Remove(TEntity item);
        void Update(TEntity item);
        Task<int> SaveChangesAsync();
        Task<List<TEntity>> GetAll();
    }
}
