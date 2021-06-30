using System;
using Mediator.Net.Contracts;

namespace NetLearningGuide.Message.Commands.Demo.FluentValidation
{
    public class DemoFluentValidationInsertCommand : ICommand
    {
        public DemoFluentValidationInsertCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}
