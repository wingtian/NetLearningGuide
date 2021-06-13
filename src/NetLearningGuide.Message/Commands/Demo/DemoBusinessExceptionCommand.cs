using Mediator.Net.Contracts;
using System;

namespace NetLearningGuide.Message.Commands.Demo
{
    public class DemoBusinessExceptionCommand : ICommand
    {
        public DemoBusinessExceptionCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
