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
                Task.Run(async () =>
                {
                    if (DeviceDisconnectHandler is null)
                    {
                        DeviceDisconnectHandler = DotNetObjectReference.Create(new DeviceDisconnectHandler(this));
                    }

                    await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.addDeviceDisconnectionHandler", DeviceDisconnectHandler, Id);
                });

                _onGattServerDisconnected += value;
            }
            remove
            {
                Task.Run(async () =>
                {
                    await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.addDeviceDisconnectionHandler", null, Id);
                });

                _onGattServerDisconnected -= value;
            }
        }

        #endregion

        #region Internal methods

        internal void RaiseOnGattServerDisconnected()
        {
            _onGattServerDisconnected?.Invoke();
        }

        #endregion
    }
}
