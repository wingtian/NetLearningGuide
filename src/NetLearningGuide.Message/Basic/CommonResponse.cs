namespace NetLearningGuide.Message.Basic
{
    public class CommonResponse<T> : ICommonResponse<T>
    {
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "OK";
        public T Data { get; set; }
    }
}
