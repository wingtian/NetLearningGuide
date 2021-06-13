namespace NetLearningGuide.Message.Basic
{
    public class CommonResponse<T> : ICommonResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
