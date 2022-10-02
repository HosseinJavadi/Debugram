using Debugram.CommonModel.Enums;

namespace Debugram.Common.CustomeException
{
    public class LogicException : AppException
    {
        public LogicException() 
            : base(ResultApiStatusCode.LogicError)
        {
        }

        public LogicException(string message) 
            : base(ResultApiStatusCode.LogicError, message)
        {
        }

        public LogicException(object additionalData) 
            : base(ResultApiStatusCode.LogicError, additionalData)
        {
        }

        public LogicException(string message, object additionalData) 
            : base(ResultApiStatusCode.LogicError, message, additionalData)
        {
        }

        public LogicException(string message, Exception exception)
            : base(ResultApiStatusCode.LogicError, message, exception)
        {
        }

        public LogicException(string message, Exception exception, object additionalData)
            : base(ResultApiStatusCode.LogicError, message, exception, additionalData)
        {
        }
    }
}
