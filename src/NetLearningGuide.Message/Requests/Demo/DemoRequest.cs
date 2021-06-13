using Mediator.Net.Contracts;

namespace NetLearningGuide.Message.Requests.Demo
{
    public class DemoRequest : IRequest
    {
        public string Id { get; set; }
    }
}
