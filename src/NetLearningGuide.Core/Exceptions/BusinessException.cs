using System;

namespace NetLearningGuide.Core.Exceptions
{
    public class BusinessException : Exception, IBusinessException
    {
        public int Code { get; set; } = 0;
    }
}
