using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.BL
{
    public abstract class QueryFilterBase<TEntity>
    {
        internal abstract IQueryable<TEntity> Apply(IQueryable<TEntity> query);
    }
}
