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

        protected readonly TaskContext _context;
        protected readonly DbSet<TEntity> _entities;
        
        public Repository(TaskContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddOrUpdate(TEntity entity)
        {
            _context.Set<TEntity>().AddOrUpdate(entity);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
          return await _context.SaveChangesAsync();
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
