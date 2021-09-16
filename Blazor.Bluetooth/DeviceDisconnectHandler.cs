using Microsoft.JSInterop;

namespace Blazor.Bluetooth
{
    internal class DeviceDisconnectHandler
    {
        private readonly Device _device;

        internal DeviceDisconnectHandler(Device device)
        {
            _device = device;
        }

        [JSInvokable]
        public void HandleDeviceDisconnected()
        {
            _device.RaiseOnGattServerDisconnected();
        }
    }
}
