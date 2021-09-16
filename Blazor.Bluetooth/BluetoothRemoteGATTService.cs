using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Bluetooth
{
    internal class BluetoothRemoteGATTService : IBluetoothRemoteGATTService
    {
        #region Intenal fields

        public string IntenalDeviceUuid { get; set; }
        public string IntenalUuid { get; set; }
        public bool IntenalIsPrimary { get; set; }

        #endregion

        #region Public fields

        public string DeviceUuid => IntenalDeviceUuid;
        public string Uuid => IntenalUuid;
        public bool IsPrimary => IntenalIsPrimary;

        #endregion

        #region Public methods

        public async Task<IBluetoothRemoteGATTCharacteristic> GetCharacteristic(string uuid)
        {
            try
            {
                var characteristic = await BluetoothNavigator.JsRuntime.InvokeAsync<BluetoothRemoteGATTCharacteristic>("ble.getCharacteristic", Uuid, uuid, DeviceUuid);
                return characteristic;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<IBluetoothRemoteGATTCharacteristic>> GetCharacteristics(string uuid)
        {
            try
            {
                var characteristics = await BluetoothNavigator.JsRuntime.InvokeAsync<List<BluetoothRemoteGATTCharacteristic>>("ble.getCharacteristics", Uuid, uuid, DeviceUuid);
                return characteristics.Select(x => (IBluetoothRemoteGATTCharacteristic)x).ToList();
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
