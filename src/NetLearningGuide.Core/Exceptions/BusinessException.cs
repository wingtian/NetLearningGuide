using System;

namespace NetLearningGuide.Core.Exceptions
{
    public class BusinessException : Exception, IBusinessException
    {
        public BusinessException(int code, string message) : base(message)
        {
            Code = code;
        }
        public int Code { get; set; }
    }
}
