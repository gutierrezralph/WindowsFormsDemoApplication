using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.BL
{
    public class CommandHandler : ICommandHandler
    {
        private readonly ITaskContext Context;
        private readonly IKernel Container;


        public CommandHandler(ITaskContext context, IKernel container)
        {
            this.Context = context;
            this.Container = container;
        }

        public TCommand Execute<TCommand>(TCommand command) where TCommand : CommandBase
        {
            Container.Inject(command);
            command.Execute(Context);
            return command;
        }
    }
}
