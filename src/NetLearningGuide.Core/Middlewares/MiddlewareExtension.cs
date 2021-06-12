using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Mediator.Net.Pipeline;
using System;

namespace NetLearningGuide.Core.Middlewares
{
    public static class MiddlewareExtension
    {
        public static void UnifyResponseMiddleware<TContext>(this IPipeConfigurator<TContext> configurator, Type unifiedType)
            where TContext : IContext<IMessage>
        {
            configurator.AddPipeSpecification(new UnifyResponseMiddlewareSpecification<TContext>(unifiedType));
        }
    }
}
