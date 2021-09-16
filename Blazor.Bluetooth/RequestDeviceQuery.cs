using Newtonsoft.Json;
using System;
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
        [JsonProperty(propertyName:"filters")]
        public List<Filter> Filters { get; set; } = new List<Filter>();

        /// <summary>
        /// Gets or sets a value indicates a boolean value indicating that the requesting script can accept all Bluetooth devices. The default is null.
        /// </summary>
        [JsonProperty(propertyName: "acceptAllDevices")]
        public bool? AcceptAllDevices { get; set; } = null;

        /// <summary>
        /// Gets or sets a list of BluetoothServiceUUIDs
        /// </summary>
        [JsonProperty(propertyName: "optionalServices")]
        public List<string> OptionalServices { get; set; } = new List<string>();

        public bool ShouldSerializeFilters()
        {
            return Filters.Count > 0;
        }

        public bool ShouldSerializeOptionalServices()
        {
            return OptionalServices.Count > 0;
        }
    }
}
