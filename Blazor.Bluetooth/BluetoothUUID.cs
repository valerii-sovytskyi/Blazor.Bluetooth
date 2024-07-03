using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// The BluetoothUUID interface of the Web Bluetooth API provides a way to look up Universally Unique Identifier (UUID) values by name in the registry maintained by the Bluetooth SIG.
    /// <seealso cref="https://developer.mozilla.org/en-US/docs/Web/API/BluetoothUUID"/>
    /// </summary>
    public static class BluetoothUUID
    {
        /// <summary>
        /// Get UUID representing a registered service when passed a name or the 16- or 32-bit UUID alias.
        /// </summary>
        /// <param name="name">A string containing the name of the service.</param>
        /// <returns>A 128-bit UUID.</returns>
        /// <exception cref="JSException">Thrown if name does not appear in the registry.</exception>
        public static async Task<string> GetService(string name)
        {
            try
            {
                var service =
                    await BluetoothNavigator.JsRuntime.InvokeAsync<string>("ble.bluetoothUUIDGetService", name);
                return service;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get UUID representing a registered characteristic when passed a name or the 16- or 32-bit UUID alias.
        /// </summary>
        /// <param name="name">A string containing the name of the characteristic.</param>
        /// <returns>A 128-bit UUID.</returns>
        /// <exception cref="JSException">Thrown if name does not appear in the registry.</exception>
        public static async Task<string> GetCharacteristic(string name)
        {
            try
            {
                var characteristic =
                    await BluetoothNavigator.JsRuntime.InvokeAsync<string>("ble.bluetoothUUIDGetCharacteristic", name);
                return characteristic;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get UUID representing a registered descriptor when passed a name or the 16- or 32-bit UUID alias.
        /// </summary>
        /// <param name="name">A string containing the name of the descriptor.</param>
        /// <returns>A 128-bit UUID.</returns>
        /// <exception cref="JSException">Thrown if name does not appear in the registry.</exception>
        public static async Task<string> GetDescriptor(string name)
        {
            try
            {
                var descriptor = await BluetoothNavigator.JsRuntime.InvokeAsync<string>("ble.bluetoothUUIDGetDescriptor", name);
                return descriptor;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get UUID representing a registered canonical when passed a name or the 16- or 32-bit UUID alias.
        /// </summary>
        /// <param name="alias">A string containing a 16-bit or 32-bit UUID alias.</param>
        /// <returns>A 128-bit UUID.</returns>
        /// <exception cref="JSException">Thrown if name does not appear in the registry.</exception>
        public static async Task<string> GetCanonicalUUID(string alias)
        {
            try
            {
                var canonical = await BluetoothNavigator.JsRuntime.InvokeAsync<string>("ble.bluetoothUUIDCanonicalUUID", alias);
                return canonical;
            }
            catch (JSException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
