using System;

namespace flash.Domain.Exceptions
{
    public class FlashException : Exception
    {
        public FlashException(string message, string code)
            : base(message)
        {
            ErrorCode = code;
        }

        public string ErrorCode { get; }
    }
}