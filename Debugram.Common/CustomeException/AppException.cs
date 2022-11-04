
using Debugram.Common.Enums;
using System.Net;

namespace Debugram.Common.CustomeException
{
    public class AppException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public ResultApiStatusCode ResultApiStatusCode { get; set; }
        public object AdditionalData { get; set; }

        public AppException()
            : this(ResultApiStatusCode.ServerError)
        {
        }

        public AppException(ResultApiStatusCode statusCode)
            : this(statusCode, null)
        {
        }

        public AppException(string message)
            : this(ResultApiStatusCode.ServerError, message)
        {
        }

        public AppException(ResultApiStatusCode statusCode, string message)
            : this(statusCode, message, HttpStatusCode.InternalServerError)
        {
        }

        public AppException(string message, object additionalData)
            : this(ResultApiStatusCode.ServerError, message, additionalData)
        {
        }

        public AppException(ResultApiStatusCode statusCode, object additionalData)
            : this(statusCode, null, additionalData)
        {
        }

        public AppException(ResultApiStatusCode statusCode, string message, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
        {
        }

        public AppException(ResultApiStatusCode statusCode, string message, HttpStatusCode httpStatusCode)
            : this(statusCode, message, httpStatusCode, null)
        {
        }

        public AppException(ResultApiStatusCode statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
            : this(statusCode, message, httpStatusCode, null, additionalData)
        {
        }

        public AppException(string message, Exception exception)
            : this(ResultApiStatusCode.ServerError, message, exception)
        {
        }

        public AppException(string message, Exception exception, object additionalData)
            : this(ResultApiStatusCode.ServerError, message, exception, additionalData)
        {
        }

        public AppException(ResultApiStatusCode statusCode, string message, Exception exception)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception)
        {
        }

        public AppException(ResultApiStatusCode statusCode, string message, Exception exception, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
        {
        }

        public AppException(ResultApiStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
            : this(statusCode, message, httpStatusCode, exception, null)
        {
        }

        public AppException(ResultApiStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
            : base(message, exception)
        {
            ResultApiStatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
            AdditionalData = additionalData;
        }
    }
}
