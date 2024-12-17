using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Bluetooth
{
    internal class BluetoothRemoteGATTServer : IBluetoothRemoteGATTServer
    {
        #region Internal fields

        public string InternalDeviceUuid { get; set; }
        public bool InternalConnected { get; set; }

        #endregion

        #region Public fields

        public string DeviceUuid => InternalDeviceUuid;
        public bool Connected => InternalConnected;

        #endregion

        #region Public methods

        public async Task Connect()
        {
            try
            {
                var device = await BluetoothNavigator.JsRuntime.InvokeAsync<Device>("ble.connectDevice", InternalDeviceUuid);
                InternalConnected = device.Gatt.Connected;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Disconnect()
        {
            try
            {
                var device = await BluetoothNavigator.JsRuntime.InvokeAsync<Device>("ble.disconnectDevice", InternalDeviceUuid);
                InternalConnected = device.Gatt.Connected;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IBluetoothRemoteGATTService> GetPrimaryService(string uuid)
        {
            try
            {
                var primaryService = await BluetoothNavigator.JsRuntime.InvokeAsync<BluetoothRemoteGATTService>("ble.getPrimaryService", uuid, DeviceUuid);
                return primaryService;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<IBluetoothRemoteGATTService>> GetPrimaryServices(string uuid)
        {
            try
            {
                var primaryServices = await BluetoothNavigator.JsRuntime.InvokeAsync<List<BluetoothRemoteGATTService>>("ble.getPrimaryServices", uuid, DeviceUuid);
                return primaryServices.Select(x => (IBluetoothRemoteGATTService)x).ToList();
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> GetConnected()
        {
            try
            {
                var device = await BluetoothNavigator.JsRuntime.InvokeAsync<Device>("ble.getDeviceById", DeviceUuid);
                InternalConnected = device.Gatt.Connected;
                return InternalConnected;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
