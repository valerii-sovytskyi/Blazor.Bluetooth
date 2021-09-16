using Microsoft.JSInterop;
using System;
using System.Linq;

namespace Blazor.Bluetooth
{
    internal class CharacteristicValueHandler
    {
        private readonly BluetoothRemoteGATTCharacteristic _characteristic;

        internal CharacteristicValueHandler(BluetoothRemoteGATTCharacteristic characteristic)
        {
            _characteristic = characteristic;
        }

        [JSInvokable]
        public void HandleCharacteristicValueChanged(Guid serviceGuid, Guid characteristicGuid, uint[] value)
        {
            byte[] byteArray = value.Select(u => (byte)(u & 0xff)).ToArray();

            var args = new CharacteristicEventArgs
            {
                ServiceId = serviceGuid,
                CharacteristicId = characteristicGuid,
                Value = byteArray
            };

            _characteristic.RaiseCharacteristicValueChanged(args);
        }
    }
}
