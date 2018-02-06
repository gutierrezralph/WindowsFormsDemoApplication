using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Common;

namespace Task.BL
{
    public class NinjectModule : Ninject.Modules.NinjectModule, ITaskNinjectModule
    {
        public bool IsRequestEnabled { get; private set; }

        public NinjectModule()
        {
            IsRequestEnabled = true;
        }

        public NinjectModule(bool isRequestEnabled)
        {
            IsRequestEnabled = isRequestEnabled;
        }

        public override void Load()
        {
            var commandHandlerBinding = Kernel.Bind<ICommandHandler>()
                .To<CommandHandler>();

            if (IsRequestEnabled)
            {
                commandHandlerBinding.InRequestScope();
            }
            else
            {
                commandHandlerBinding.InTransientScope();
            }

        }
    }
}
