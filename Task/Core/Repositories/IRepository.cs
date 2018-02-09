using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Awaitable = System.Threading.Tasks;

namespace Task.Core.Repositories
{
    public interface IRepository
    {

    }
    public interface IRepository <TEntity> : IRepository where TEntity : class
    {
        Awaitable.Task Add(TEntity entity);
        Awaitable.Task<TEntity> GetAsync(int id);
        Awaitable.Task<IEnumerable<TEntity>> GetAllAsync();
        Awaitable.Task SaveOrUpdate(TEntity entity);
        Awaitable.Task DeleteOnSubmit(TEntity entity);
    }
}
