using Debugram.CommonModel.Enums;

namespace Debugram.Common.CustomeException
{
    public class BadRequestException : AppException
    {
        public BadRequestException()
            : base(ResultApiStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(ResultApiStatusCode.BadRequest, message)
        {
        }

        public BadRequestException(object additionalData)
            : base(ResultApiStatusCode.BadRequest, additionalData)
        {
        }

        public BadRequestException(string message, object additionalData)
            : base(ResultApiStatusCode.BadRequest, message, additionalData)
        {
        }

        public BadRequestException(string message, Exception exception)
            : base(ResultApiStatusCode.BadRequest, message, exception)
        {
        }

        public BadRequestException(string message, Exception exception, object additionalData)
            : base(ResultApiStatusCode.BadRequest, message, exception, additionalData)
        {
        }
    }
}
