using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.BL
{
    public interface IQueryHandler
    {
        TQuery Execute<TQuery>(TQuery query) where TQuery : QueryBase;
    }
}
