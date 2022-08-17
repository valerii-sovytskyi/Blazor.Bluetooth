using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Bluetooth
{
    internal class BluetoothRemoteGATTCharacteristic : IBluetoothRemoteGATTCharacteristic
    {
        #region Private fields

        private DotNetObjectReference<CharacteristicValueHandler> _characteristicValueHandler;
        private event EventHandler<CharacteristicEventArgs> _onRaiseCharacteristicValueChanged;

        #endregion

        #region Internal fields

        public string InternalDeviceUuid { get; set; }
        public string InternalServiceUuid { get; set; }
        public string InternalUuid { get; set; }
        public BluetoothCharacteristicProperties InternalProperties { get; set; }
        public byte[] InternalValue { get; set; }

        #endregion

        #region Public fields

        public string DeviceUuid => InternalDeviceUuid;
        public string ServiceUuid => InternalServiceUuid;
        public string Uuid => InternalUuid;
        public IBluetoothCharacteristicProperties Properties => InternalProperties;
        public byte[] Value => InternalValue;

        public event EventHandler<CharacteristicEventArgs> OnRaiseCharacteristicValueChanged
        {
            add
            {
                if (_characteristicValueHandler == null)
                {
                    _characteristicValueHandler = DotNetObjectReference.Create(new CharacteristicValueHandler(this));
                }

                BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.setCharacteristicValueChangedHandler", _characteristicValueHandler);

                _onRaiseCharacteristicValueChanged += value;
            }
            remove
            {
                BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.setCharacteristicValueChangedHandler", null);

                _onRaiseCharacteristicValueChanged -= value;
            }
        }

        #endregion

        #region Public methods

        public async Task<IBluetoothRemoteGATTDescriptor> GetDescriptor(string uuid)
        {
            try
            {
                var descriptor = await BluetoothNavigator.JsRuntime.InvokeAsync<BluetoothRemoteGATTDescriptor>("ble.getDescriptor", uuid, ServiceUuid, Uuid, DeviceUuid);
                return descriptor;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<IBluetoothRemoteGATTDescriptor>> GetDescriptors(string uuid)
        {
            try
            {
                var descriptors = await BluetoothNavigator.JsRuntime.InvokeAsync<List<BluetoothRemoteGATTDescriptor>>("ble.getDescriptors", uuid, ServiceUuid, Uuid, DeviceUuid);
                return descriptors.Select(x => (IBluetoothRemoteGATTDescriptor)x).ToList();
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<byte[]> ReadValue()
        {
            try
            {
                var value = await BluetoothNavigator.JsRuntime.InvokeAsync<uint[]>("ble.characteristicReadValue", DeviceUuid, ServiceUuid, Uuid);
                return value.Select(v => (byte)(v & 0xFF)).ToArray();
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [Obsolete("This feature is no longer recommended. Though some browsers might still support it, it may have already been removed from the relevant web standards, may be in the process of being dropped, or may only be kept for compatibility purposes. Avoid using it, and update existing code if possible; see the compatibility table at the bottom of this page to guide your decision. Be aware that this feature may cease to work at any time.")]
        public async Task WriteValue(byte[] value)
        {
            var bytes = value.Select(v => (uint)v).ToArray();

            try
            {
                await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.characteristicWriteValue", DeviceUuid, ServiceUuid, Uuid, bytes);

            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task WriteValueWithoutResponse(byte[] value)
        {
            var bytes = value.Select(v => (uint)v).ToArray();

            try
            {
                await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.characteristicWriteValueWithoutResponse", DeviceUuid, ServiceUuid, Uuid, bytes);

            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task WriteValueWithResponse(byte[] value)
        {
            var bytes = value.Select(v => (uint)v).ToArray();

            try
            {
                await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.characteristicWriteValueWithResponse", DeviceUuid, ServiceUuid, Uuid, bytes);
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task StartNotifications()
        {
            try
            {
                await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.startNotification", DeviceUuid, ServiceUuid, Uuid);
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task StopNotifications()
        {
            try
            {
                await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.stopNotification", DeviceUuid, ServiceUuid, Uuid);
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Internal methods

        internal void RaiseCharacteristicValueChanged(CharacteristicEventArgs args)
        {
            _onRaiseCharacteristicValueChanged?.Invoke(this, args);
        }

        #endregion
    }
}
