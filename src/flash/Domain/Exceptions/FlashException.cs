using System;

namespace flash
{
    public class FlashException : Exception
    {
        public FlashException(string message)
            : base(message)
        {
        }
    }
}