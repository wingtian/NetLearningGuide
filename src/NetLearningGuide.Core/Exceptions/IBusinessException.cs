namespace NetLearningGuide.Core.Exceptions
{
    public interface IBusinessException
    {
        //LogEventLevel LogLevel { get; }

        int Code { get; set; }

        string Message { get; }
    }
}
