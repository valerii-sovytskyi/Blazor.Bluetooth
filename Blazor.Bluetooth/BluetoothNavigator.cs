using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Blazor.Bluetooth
{
    internal class BluetoothNavigator : IBluetoothNavigator
    {
        #region Private fields

        private DotNetObjectReference<BluetoothAvailabilityHandler> BluetoothAvailabilityHandler;

        #endregion

        #region Internal fields

        /// <summary>
        /// TODO: find out the way to not use static.
        /// </summary>
        internal static IJSRuntime JsRuntime { get; private set; }

        #endregion

        #region Public fields

        private event Action _onAvailabilityChanged;

        public event Action OnAvailabilityChanged
        {
            add
            {
                if (BluetoothAvailabilityHandler is null)
                {
                    BluetoothAvailabilityHandler = DotNetObjectReference.Create(new BluetoothAvailabilityHandler(this));
                }

                JsRuntime.InvokeVoidAsync("ble.addBluetoothAvailabilityHandler", BluetoothAvailabilityHandler);

                _onAvailabilityChanged += value;
            }
            remove
            {
                JsRuntime.InvokeVoidAsync("ble.addBluetoothAvailabilityHandler", null);

                _onAvailabilityChanged -= value;
            }
        }

        #endregion

        #region Constructor

        public BluetoothNavigator(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        #endregion

        #region Public methods

        public async Task<IDevice> ReferringDevice()
        {
            try
            {
                var device = await JsRuntime.InvokeAsync<Device>("ble.referringDevice");
                return device;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> GetAvailability()
        {
            try
            {
                var isBleAvailable = await JsRuntime.InvokeAsync<bool>("ble.getAvailability");
                return isBleAvailable;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<IDevice>> GetDevices()
        {
            try
            {
                var devices = await JsRuntime.InvokeAsync<Device[]>("ble.getDevices");
                return devices.Select(x => (IDevice)x).ToList();
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IDevice> RequestDevice(RequestDeviceQuery query)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            };

            var json = JsonSerializer.Serialize(query, jsonOptions);

            try
            {
                var device = await JsRuntime.InvokeAsync<Device>("ble.requestDevice", json);
                return device;
            }
            catch (JSException ex)
            {
                if (ex.Message.Contains("User cancelled the requestDevice() chooser."))
                {
                    throw new RequestDeviceCancelledException(ex.Message, ex);
                }

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Internal methods

        internal void RaiseOnAvailabilityChanged()
        {
            _onAvailabilityChanged?.Invoke();
        }

        #endregion
    }
}
