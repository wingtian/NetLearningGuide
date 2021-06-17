using System;
using Mediator.Net.Contracts;

namespace NetLearningGuide.Message.Requests.Demo
{
    public class DemoEfQueryRequest : IRequest
    {
        public DemoEfQueryRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
