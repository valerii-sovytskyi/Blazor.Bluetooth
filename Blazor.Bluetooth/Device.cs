using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Represents a Bluetooth device inside a particular script execution environment.
    /// </summary>
    internal class Device : IDevice
    {
        #region Private fields

        private DotNetObjectReference<DeviceDisconnectHandler> DeviceDisconnectHandler;
        private DotNetObjectReference<AdvertisementReceivedHandler> AdvertisementReceivedHandler;

        #endregion

        #region Internal fields

        public string InternalId { get; set; }

        public string InternalName { get; set; }

        public BluetoothRemoteGATTServer InternalGatt { get; set; }

        #endregion

        #region Public fields

        public string Id => InternalId;

        public string Name => InternalName;

        public IBluetoothRemoteGATTServer Gatt => InternalGatt;

        private event Action _onGattServerDisconnected;
        public event Action OnGattServerDisconnected
        {
            add
            {
                if (DeviceDisconnectHandler is null)
                {
                    DeviceDisconnectHandler = DotNetObjectReference.Create(new DeviceDisconnectHandler(this));
                }

                BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.addDeviceDisconnectionHandler", DeviceDisconnectHandler, Id);

                _onGattServerDisconnected += value;
            }
            remove
            {
                BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.addDeviceDisconnectionHandler", null, Id);

                _onGattServerDisconnected -= value;
            }
        }

        private event Action<IBluetoothAdvertisingEvent> _onAdvertisementReceived;
        public event Action<IBluetoothAdvertisingEvent> OnAdvertisementReceived
        {
            add
            {
                if (AdvertisementReceivedHandler is null)
                {
                    AdvertisementReceivedHandler = DotNetObjectReference.Create(new AdvertisementReceivedHandler(this));
                }

                BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.setAdvertisementReceivedHandler", AdvertisementReceivedHandler, Id);

                _onAdvertisementReceived += value;
            }
            remove
            {
                BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.setAdvertisementReceivedHandler", null, Id);

                _onAdvertisementReceived -= value;
            }
        }

        #endregion

        #region Public methods

        public async Task WatchAdvertisements()
        {
            try
            {
                await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.watchAdvertisements", InternalId);
            }
            catch (JSException ex)
            {
                if (ex.Message.Contains("watchAdvertisements is not a function"))
                {
                    throw new AdvertisementsUnavailableException(ex);
                }

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Internal methods

        internal void RaiseOnGattServerDisconnected()
        {
            _onGattServerDisconnected?.Invoke();
        }

        internal void RaiseAdvertisementReceived(BluetoothAdvertisingEvent bluetoothAdvertisingEvent)
        {
            _onAdvertisementReceived?.Invoke(bluetoothAdvertisingEvent);
        }

        #endregion
    }
}
