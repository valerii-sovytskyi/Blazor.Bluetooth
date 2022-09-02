using System;

namespace Blazor.Bluetooth
{
    public class BluetoothNotSupportedException : Exception
    {
        public BluetoothNotSupportedException(Exception innerException)
            : base("Bluetooth probably is not supported on your browser. Please check browser compatibility https://developer.mozilla.org/en-US/docs/Web/API/Bluetooth#browser_compatibility", innerException)
        {
        }
    }
}
