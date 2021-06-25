using Mediator.Net.Contracts;
using System;

namespace NetLearningGuide.Message.Commands.Demo
{
    public class DemoEfInsertCommand : ICommand
    {
        public DemoEfInsertCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}
