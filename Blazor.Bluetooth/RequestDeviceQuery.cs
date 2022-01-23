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
        /// Gets or sets a value indicates a boolean value indicating that the requesting script can accept all Bluetooth devices. The default is null.
        /// </summary>
        [JsonPropertyName("acceptAllDevices")]
        public bool? AcceptAllDevices { get; set; } = null;

        /// <summary>
        /// Gets or sets a list of BluetoothServiceUUIDs
        /// </summary>
        [JsonPropertyName("optionalServices")]
        public List<string> OptionalServices { get; set; }
    }
}
