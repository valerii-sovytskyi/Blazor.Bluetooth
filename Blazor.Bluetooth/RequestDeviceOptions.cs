using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Request device options for <see cref="IBluetoothNavigator.RequestDevice(RequestDeviceOptions)"/>.
    /// </summary>
    [Serializable]
    public class RequestDeviceOptions
    {
        /// <summary>
        /// Gets or sets a filters.
        /// </summary>
        [JsonPropertyName("filters")]
        public List<Filter>? Filters { get; set; } = null;

        /// <summary>
        /// Gets or sets a list of BluetoothServiceUUIDs
        /// An array of optional service identifiers.
        /// 
        /// The identifiers take the same values as the elements of the services array (a GATT service name, service UUID, or UUID short 16-bit or 32-bit form).
        /// </summary>
        [JsonPropertyName("optionalServices")]
        public List<string>? OptionalServices { get; set; } = null;
        
        /// <summary>
        /// Gets or sets a Manufacturer data.
        /// An array of objects matching against manufacturer data in the Bluetooth Low Energy (BLE) advertising packets.
        /// </summary>
        [JsonPropertyName("optionalManufacturerData")]
        public List<ManufacturerData>? OptionalManufacturerData { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicates a boolean value indicating that the requesting script can accept all Bluetooth devices. The default is null.
        /// 
        /// This option is appropriate when devices have not advertised enough information for filtering to be useful.
        /// When acceptAllDevices is set to true you should omit all filters and exclusionFilters,
        /// and you must set optionalServices to be able to use the returned device.
        /// </summary>
        [JsonPropertyName("acceptAllDevices")]
        public bool? AcceptAllDevices { get; set; } = null;
    }
}
