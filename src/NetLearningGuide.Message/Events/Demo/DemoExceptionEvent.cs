using Mediator.Net.Contracts;
using System;

namespace NetLearningGuide.Message.Events.Demo
{
    public class DemoExceptionEvent : IEvent
    {
        public DemoExceptionEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
