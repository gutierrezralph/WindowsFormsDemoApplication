using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.BL
{
    public abstract class QueryBase
    {
        internal abstract void Execute(ITaskContext context);
    }

    public abstract class QueryBase<TResult> : QueryBase
        where TResult : class
    {
        public TResult Result { get; protected set; }
    }

    public abstract class QueryBase<TResult, TEntity> : QueryBase
        where TResult : class
        where TEntity : class, IEntity // IEntity
    {
        protected IList<QueryFilterBase<TEntity>> filters;

        public TResult Result { get; protected set; }

        protected QueryBase()
        {
            filters = new List<QueryFilterBase<TEntity>>();
        }

        public QueryBase<TResult, TEntity> AddFilter(params QueryFilterBase<TEntity>[] filters)
        {
            foreach (var filter in filters)
            {
                this.filters.Add(filter);
            }

            return this;
        }

        public QueryBase<TResult, TEntity> AddFilter(QueryFilterBase<TEntity> filter)
        {
            filters.Add(filter);

            return this;
        }

        protected IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query)
        {
            var resultQuery = filters.Aggregate(query, (current, filter) => filter.Apply(current));

            return resultQuery;
        }
    }

    public abstract class SimpleQueryBase<TResult, TEntity> : QueryBase where TEntity : class, IEntity
    {
        protected IList<QueryFilterBase<TEntity>> filters;

        public TResult Result { get; protected set; }

        protected SimpleQueryBase()
        {
            filters = new List<QueryFilterBase<TEntity>>();
        }

        public SimpleQueryBase<TResult, TEntity> AddFilter(params QueryFilterBase<TEntity>[] filters)
        {
            foreach (var filter in filters)
            {
                this.filters.Add(filter);
            }

            return this;
        }

        public SimpleQueryBase<TResult, TEntity> AddFilter(QueryFilterBase<TEntity> filter)
        {
            filters.Add(filter);

            return this;
        }

        protected IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query)
        {
            var resultQuery = filters.Aggregate(query, (current, filter) => filter.Apply(current));

            return resultQuery;
        }
    }
}
