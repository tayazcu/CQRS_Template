using System;

namespace Project.Framework.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException()
            : base(StatusCode.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(StatusCode.BadRequest, message)
        {
        }

        public BadRequestException(object additionalData)
            : base(StatusCode.BadRequest, additionalData)
        {
        }

        public BadRequestException(string message, object additionalData)
            : base(StatusCode.BadRequest, message, additionalData)
        {
        }

        public BadRequestException(string message, Exception exception)
            : base(StatusCode.BadRequest, message, exception)
        {
        }

        public BadRequestException(string message, Exception exception, object additionalData)
            : base(StatusCode.BadRequest, message, exception, additionalData)
        {
        }
    }
}
