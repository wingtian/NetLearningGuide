using Mediator.Net.Contracts;
using System;

namespace NetLearningGuide.Message.Commands.Demo
{
    public class DemoExceptionCommand : ICommand
    {
        public DemoExceptionCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
