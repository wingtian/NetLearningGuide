using Mediator.Net.Contracts;
using System;

namespace NetLearningGuide.Message.Events.Demo
{
    public class DemoEvent : IEvent
    {
        public DemoEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
