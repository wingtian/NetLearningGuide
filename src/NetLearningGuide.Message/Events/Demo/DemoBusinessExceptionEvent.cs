using Mediator.Net.Contracts;
using System;

namespace NetLearningGuide.Message.Events.Demo
{
    public class DemoBusinessExceptionEvent : IEvent
    {
        public DemoBusinessExceptionEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
