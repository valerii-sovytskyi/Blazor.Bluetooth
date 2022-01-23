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
                await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.connectCurrentDevice");
                var currentDevice = await BluetoothNavigator.JsRuntime.InvokeAsync<Device>("ble.getCurrentDevice");
                InternalConnected = currentDevice.Gatt.Connected;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Disonnect()
        {
            try
            {
                await BluetoothNavigator.JsRuntime.InvokeVoidAsync("ble.disconnectCurrentDevice");
                var currentDevice = await BluetoothNavigator.JsRuntime.InvokeAsync<Device>("ble.getCurrentDevice");
                InternalConnected = currentDevice.Gatt.Connected;
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
            var currentDevice = await BluetoothNavigator.JsRuntime.InvokeAsync<Device>("ble.getCurrentDevice");
            InternalConnected = currentDevice.Gatt.Connected;
            return InternalConnected;
        }

        #endregion
    }
}
