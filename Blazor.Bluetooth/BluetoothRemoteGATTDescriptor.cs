using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Bluetooth
{
    internal class BluetoothRemoteGATTDescriptor : IBluetoothRemoteGATTDescriptor
    {
        #region Internal fields

        public string InternalDeviceUuid { get; set; }
        public string InternalServiceUuid { get; set; }
        public string InternalCharacteristicUuid { get; set; }
        public string InternalUuid { get; set; }
        public byte[] InternalValue { get; set; }

        #endregion

        #region Public fields

        public string DeviceUuid => InternalDeviceUuid;
        public string ServiceUuid => InternalServiceUuid;
        public string CharacteristicUuid => InternalCharacteristicUuid;
        public string Uuid => InternalUuid;
        public byte[] Value => InternalValue;

        #endregion

        #region Public methods

        public async Task<byte[]> ReadValue()
        {
            try
            {
                var value = await BluetoothNavigator.JsRuntime.InvokeAsync<uint[]>("ble.descriptorReadValue", DeviceUuid, ServiceUuid, CharacteristicUuid, Uuid);
                return value.Select(v => (byte)(v & 0xFF)).ToArray();
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task WriteValue(byte[] value)
        {
            var bytes = value.Select(v => (uint)v).ToArray();

            try
            {
                await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.descriptorWriteValue", DeviceUuid, ServiceUuid, CharacteristicUuid, Uuid, bytes);
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
