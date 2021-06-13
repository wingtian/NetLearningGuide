﻿using Mediator.Net.Context;
using Mediator.Net.Contracts;
using Mediator.Net.Pipeline;
using NetLearningGuide.Core.Exceptions;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Middlewares
{
    public class UnifyResponseMiddlewareSpecification<TContext> : IPipeSpecification<TContext>
        where TContext : IContext<IMessage>
    {
        private readonly Type _unifiedType;
        public UnifyResponseMiddlewareSpecification(Type unifiedType)
        {
            _unifiedType = unifiedType;
        }

        public Task AfterExecute(TContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task BeforeExecute(TContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Execute(TContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task OnException(Exception ex, TContext context)
        {
            if (_unifiedType == null || ex.GetType() != typeof(BusinessException) || context.Message is IEvent)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                throw ex;
            }
            var businessException = ex as BusinessException;
            var targetType = context.ResultDataType;
            var unifiedTypeInstance = Activator.CreateInstance(targetType) as dynamic;
            unifiedTypeInstance.Code = businessException.Code;
            unifiedTypeInstance.Message = businessException.Message;
            context.Result = unifiedTypeInstance;
            //TODO: add Serilog
            //Log.Error(JsonConvert.SerializeObject(unifiedTypeInstance));
            return Task.CompletedTask;
        }

        public bool ShouldExecute(TContext context, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
