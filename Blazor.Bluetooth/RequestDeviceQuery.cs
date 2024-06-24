using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Request device query for <see cref="IBluetoothNavigator.RequestDevice(RequestDeviceQuery)"/>.
    /// </summary>
    [Serializable]
    public class RequestDeviceQuery
    {
        /// <summary>
        /// Gets or sets a filters.
        /// </summary>
        [JsonPropertyName("filters")]
        public List<Filter> Filters { get; set; }

        /// <summary>
        /// Gets or sets a list of BluetoothServiceUUIDs
        /// An array of optional service identifiers.
        /// 
        /// The identifiers take the same values as the elements of the services array (a GATT service name, service UUID, or UUID short 16-bit or 32-bit form).
        /// </summary>
        [JsonPropertyName("optionalServices")]
        public List<string> OptionalServices { get; set; } = null;
        
        /// <summary>
        /// Gets or sets a string of hex numbers, write them through the comma and without brakes.
        ///
        /// The data is not used for filtering the devices,
        /// but advertisements that match the specified set are still delivered in advertisementreceived events.
        /// This is useful because it allows code to specify an interest in data received
        /// from Bluetooth devices without constraining the filter controlling
        /// which devices are presented to the user in the permission prompt.
        /// </summary>
        /// <example>0x0CFD, 0x0B07, 0x0A29</example>
        /// <remarks>Use this link to see full lists of company numbers https://www.bluetooth.com/specifications/assigned-numbers/.</remarks>
        [JsonPropertyName("optionalManufacturerData")]
        public List<int> OptionalManufacturerData { get; set; } = null;

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
