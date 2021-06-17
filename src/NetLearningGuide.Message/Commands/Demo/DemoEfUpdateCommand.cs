using Mediator.Net.Contracts;
using System;

namespace NetLearningGuide.Message.Commands.Demo
{
    public class DemoEfUpdateCommand : ICommand
    {
        public DemoEfUpdateCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
