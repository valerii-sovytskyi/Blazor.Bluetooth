using Microsoft.JSInterop;

namespace Blazor.Bluetooth
{
    internal class BluetoothAvailabilityHandler
    {
        private readonly BluetoothNavigator _bluetoothNavigator;

        internal BluetoothAvailabilityHandler(BluetoothNavigator bluetoothNavigator)
        {
            _bluetoothNavigator = bluetoothNavigator;
        }

        [JSInvokable]
        public void HandleAvailabilityChanged()
        {
            _bluetoothNavigator.RaiseOnAvailabilityChanged();
        }
    }
}
