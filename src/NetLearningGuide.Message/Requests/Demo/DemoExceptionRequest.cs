using Mediator.Net.Contracts;

namespace NetLearningGuide.Message.Requests.Demo
{
    public class DemoExceptionRequest : IRequest
    {
        public string Id { get; set; }
    }
}
