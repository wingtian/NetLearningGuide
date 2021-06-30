using Mediator.Net.Contracts;
using System;

namespace NetLearningGuide.Message.Commands.Demo.FluentValidation
{
    public class DemoFluentValidationUpdateCommand : ICommand
    {
        public DemoFluentValidationUpdateCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
