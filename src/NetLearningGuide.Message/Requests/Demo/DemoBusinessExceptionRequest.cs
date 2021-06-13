using Mediator.Net.Contracts;

namespace NetLearningGuide.Message.Requests.Demo
{
    public class DemoBusinessExceptionRequest : IRequest
    {
        public string Id { get; set; }
    }
}
