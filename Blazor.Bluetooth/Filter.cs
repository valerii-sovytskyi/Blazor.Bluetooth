using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Filter for <see cref="RequestDeviceOptions"/>.
    /// </summary>
    [Serializable]
    public class Filter
    {
        /// <summary>
        /// Gets or sets Name of a device.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets Prefix name.
        /// </summary>
        [JsonPropertyName("namePrefix")]
        public string? NamePrefix { get; set; }

        /// <summary>
        /// Gets or sets service objects. For example those types possible: 'heart_rate', 0x1802, 0x1803, 'c48e6067-5295-48d3-8d5c-0395f61792b1'.
        /// </summary>
        [JsonPropertyName("services")]
        public List<string>? Services { get; set; } = null;
        
        /// <summary>
        /// Gets or sets a Manufacturer data.
        /// An array of objects matching against manufacturer data in the Bluetooth Low Energy (BLE) advertising packets.
        /// </summary>
        [JsonPropertyName("manufacturerData")]
        public List<ManufacturerData>? ManufacturerData { get; set; } = null;
    }
}
