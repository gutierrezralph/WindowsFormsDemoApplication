using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Task.Core.Domain;

namespace Task
{
    public interface ITaskContext : IDisposable , IObjectContextAdapter
    {
         IDbSet<Employee> Employees { get; set; }

        int SaveChanges();
        Database Database { get; }
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet Set(Type entityType);
    }
}
