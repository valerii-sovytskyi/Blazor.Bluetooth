using System.Text.Json.Serialization;

namespace Blazor.Bluetooth
{
    public class ManufacturerData
    {
        /// <summary>
        /// Gets or sets a company identifier.
        /// 
        /// A mandatory number identifying the manufacturer of the device.
        /// Company identifiers are listed in the Bluetooth specification <see cref="https://www.bluetooth.com/specifications/assigned-numbers/" />, Section 7.
        /// For example, to match against devices manufacturered by "Digianswer A/S",
        /// with assigned hex number 0x000C, you would specify 12.
        /// </summary>
        [JsonPropertyName("companyIdentifier")]
        public int CompanyIdentifier { get; set; }
        
        /// <summary>
        /// Gets or sets a data prefix.
        /// Optional.
        /// 
        /// The data prefix.
        /// A buffer containing values to match against the values at the start
        /// of the advertising manufacturer data.
        /// </summary>
        [JsonPropertyName("dataPrefix")]
        public byte[]? DataPrefix { get; set; } = null;
        
        /// <summary>
        /// Gets or sets a mask.
        /// Optional.
        ///
        /// This allows you to match against bytes within the manufacturer data,
        /// by masking some bytes of the service data dataPrefix.
        /// </summary>
        [JsonPropertyName("mask")]
        public byte[]? Mask { get; set; } = null;
    }
}
