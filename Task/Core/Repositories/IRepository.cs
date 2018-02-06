using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.Repositories
{
    public interface IRepository
    {

    }
    public interface IRepository <TEntity> : IRepository where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void SaveOrUpdate(TEntity entity);
        Task<int> SaveChangesAsync();
        void DeleteOnSubmit(TEntity entity);
    }
}
