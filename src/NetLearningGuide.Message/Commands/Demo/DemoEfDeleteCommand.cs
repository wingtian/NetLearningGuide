using Mediator.Net.Contracts;
using System;

namespace NetLearningGuide.Message.Commands.Demo
{
    public class DemoEfDeleteCommand : ICommand
    {
        public DemoEfDeleteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
