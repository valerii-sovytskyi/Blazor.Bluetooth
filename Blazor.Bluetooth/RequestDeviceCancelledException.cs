using System;

namespace Blazor.Bluetooth
{
    public class RequestDeviceCancelledException : Exception
    {
        public RequestDeviceCancelledException()
        {
        }

        public RequestDeviceCancelledException(string message)
            : base(message)
        {
        }

        public RequestDeviceCancelledException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
