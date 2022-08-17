using Microsoft.JSInterop;
using System.Text.Json;

namespace Blazor.Bluetooth
{
    internal class AdvertisementReceivedHandler
    {
        private readonly Device _device;

        internal AdvertisementReceivedHandler(Device device)
        {
            _device = device;
        }

        [JSInvokable]
        public void HandleAdvertisementReceived(JsonElement jsonElement)
        {
            var json = jsonElement.GetRawText();
            var bluetoothAdvertisingEvent = JsonSerializer.Deserialize<BluetoothAdvertisingEvent>(json);
            _device.RaiseAdvertisementReceived(bluetoothAdvertisingEvent);
        }
    }

}
