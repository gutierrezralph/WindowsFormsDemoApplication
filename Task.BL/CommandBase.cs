using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.BL
{
    public abstract class CommandBase
    {
        internal abstract void Execute(ITaskContext context);
    }

    public abstract class CommandBase<TResult> : CommandBase
    {
        internal virtual async Task<TResult> ExecuteAsyncResult(ITaskContext context)
        {
            throw new NotSupportedException("Async result call is not supported.");
        }

        public TResult Result { get; protected set; }
    }

}
