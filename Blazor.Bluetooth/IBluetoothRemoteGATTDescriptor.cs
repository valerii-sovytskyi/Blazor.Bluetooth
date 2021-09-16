using System.Threading.Tasks;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Represents a GATT Descriptor, which provides further information about a characteristic’s value.
    /// </summary>
    public interface IBluetoothRemoteGATTDescriptor
    {
        /// <summary>
        /// Gets a string that uniquely identifies a device.
        /// </summary>
        string DeviceUuid { get; }

        /// <summary>
        /// Gets a string representing the UUID of this service.
        /// </summary>
        string ServiceUuid { get; }

        /// <summary>
        /// Gets a string containing the UUID of the characteristic, for example '00002a37-0000-1000-8000-00805f9b34fb' for the Heart Rate Measurement characteristic.
        /// </summary>
        string CharacteristicUuid { get; }

        /// <summary>
        /// Gets the UUID of the characteristic descriptor, for example '00002902-0000-1000-8000-00805f9b34fb' for theClient Characteristic Configuration descriptor.
        /// </summary>
        string Uuid { get; }

        /// <summary>
        /// Gets the currently cached descriptor value. This value gets updated when the value of the descriptor is read.
        /// </summary>
        byte[] Value { get; }

        /// <summary>
        /// Read the value property if it is available and supported. Otherwise it throws an error.
        /// </summary>
        /// <returns>Task with bytes array.</returns>
        Task<byte[]> ReadValue();

        /// <summary>
        /// Sets the value into descriptor.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <returns>Task.</returns>
        Task WriteValue(byte[] value);
    }
}
