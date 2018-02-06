using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.BL
{
    public interface ICommandHandler
    {
        TCommand Execute<TCommand>(TCommand command)
                   where TCommand : CommandBase;
    }
}
