using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task.Core.Repositories;

namespace Task.Implementation.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class , IEntity
    {

        protected readonly TaskContext Context;
        protected readonly DbSet<TEntity> entities;
        
        public Repository(TaskContext context)
        {
            Context = context;
            entities = context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddOrUpdate(TEntity entity)
        {
            Context.Set<TEntity>().AddOrUpdate(entity);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public void Remove(TEntity entity)
        {
           Context.Set<TEntity>().Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
          return await Context.SaveChangesAsync();
        }        

        public void SaveOrUpdate(TEntity entity)
        {
            AddOrUpdate(entity);
        }

        public void DeleteOnSubmit(TEntity entity)
        {
            Remove(entity);
        }
    }
}
