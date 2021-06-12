using Mediator.Net.Contracts;

namespace NetLearningGuide.Message.Basic
{
    public interface ICommonResponse<T> : IResponse
    {
        int Code { get; set; }

        string Message { get; set; }

        T Data { get; set; }
    }
}
