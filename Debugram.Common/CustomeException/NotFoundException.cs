using Debugram.Common.Enums;

namespace Debugram.Common.CustomeException
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
            : base(ResultApiStatusCode.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(ResultApiStatusCode.NotFound, message)
        {
        }

        public NotFoundException(object additionalData)
            : base(ResultApiStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, object additionalData)
            : base(ResultApiStatusCode.NotFound, message, additionalData)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(ResultApiStatusCode.NotFound, message, exception)
        {
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(ResultApiStatusCode.NotFound, message, exception, additionalData)
        {
        }
    }
}
