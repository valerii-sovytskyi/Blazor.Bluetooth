using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Filter for <see cref="RequestDeviceQuery"/>.
    /// </summary>
    [Serializable]
    public class Filter
    {
        /// <summary>
        /// Gets or sets service objects. For example those types possible: 'heart_rate', 0x1802, 0x1803, 'c48e6067-5295-48d3-8d5c-0395f61792b1'.
        /// </summary>
        [JsonProperty(propertyName: "services")]
        public List<object> Services { get; set; } = new List<object>();

        /// <summary>
        /// Gets or sets Name of a device.
        /// </summary>
        [JsonProperty(propertyName: "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Prefix name.
        /// </summary>
        [JsonProperty(propertyName: "namePrefix")]
        public string NamePrefix { get; set; }

        public bool ShouldSerializeServices()
        {
            return Services.Count > 0;
        }
    }
}
