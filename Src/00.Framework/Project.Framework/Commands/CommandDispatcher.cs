using Project.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Project.Framework.Commands
{
    public class CommandDispatcher : ITransientDependencySingle
    {
        private readonly IServiceProvider _provider;

        public CommandDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public CommandResult Dispatch(ICommand command)
        {
            Type type = typeof(CommandHandler<>);
            Type[] typeArgs = { command.GetType() };
            Type handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = _provider.GetService(handlerType);
            CommandResult result = handler.Handle((dynamic)command);
            return result;
        }
    }
}
