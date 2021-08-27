using System;

namespace flash
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