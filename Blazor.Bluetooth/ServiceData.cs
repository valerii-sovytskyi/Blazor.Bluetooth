using System.Text.Json.Serialization;

namespace Blazor.Bluetooth
{
    public class ServiceData
    {
        /// <summary>
        /// Gets or sets a service identifier.
        /// 
        /// The GATT service name, the service UUID, or the UUID 16-bit or 32-bit form.
        /// This takes the same values as the elements of the services array.
        /// </summary>
        [JsonPropertyName("service")]
        public string Service { get; set; } = null;
        
        /// <summary>
        /// Gets or sets a data prefix.
        /// Optional.
        /// 
        /// The data prefix.
        /// A buffer containing values to match against the values at the start of the advertising service data.
        /// </summary>
        [JsonPropertyName("dataPrefix")]
        public byte[]? DataPrefix { get; set; } = null;
        
        /// <summary>
        /// Gets or sets a mask.
        /// Optional.
        ///
        /// This allows you to match against bytes within the service data,
        /// by masking some bytes of the service data dataPrefix.
        /// </summary>
        [JsonPropertyName("mask")]
        public byte[]? Mask { get; set; } = null;
    }
}
