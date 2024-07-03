using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Represents a service provided by a GATT server, including a device, a list of referenced services, and a list of the characteristics of this service.
    /// </summary>
    public interface IBluetoothRemoteGATTService
    {
        /// <summary>
        /// Gets a string that uniquely identifies a device.
        /// </summary>
        string DeviceUuid { get; }

        /// <summary>
        /// Gets a string representing the UUID of this service.
        /// </summary>
        string Uuid { get; }

        /// <summary>
        /// Gets a boolean value indicating whether this is a primary or secondary service.
        /// </summary>
        bool IsPrimary { get; }

        /// <summary>
        /// Returns an instance of <see cref="IBluetoothRemoteGATTCharacteristic"/> for a given universally unique identifier (UUID).
        /// </summary>
        /// <param name="uuid">The UUID of a characteristic, for example '00002a37-0000-1000-8000-00805f9b34fb' for the Heart Rate Measurement characteristic.</param>
        /// <returns>Task with <see cref="IBluetoothCharacteristicProperties"/> result.</returns>
        Task<IBluetoothRemoteGATTCharacteristic> GetCharacteristic(string uuid);
        
        /// <summary>
        /// Returns a list of BluetoothRemoteGATTCharacteristic instances for a given universally unique identifier (UUID).
        /// </summary>
        /// <param name="uuid">The UUID of a characteristic, for example '00002a37-0000-1000-8000-00805f9b34fb' for the Heart Rate Measurement characteristic.</param>
        /// <returns>Task with list of <see cref="IBluetoothRemoteGATTCharacteristic"/> result.</returns>
        Task<List<IBluetoothRemoteGATTCharacteristic>> GetCharacteristics(string uuid);
        
        /// <summary>
        /// Returns a list of BluetoothRemoteGATTCharacteristic instances for a given universally unique identifier (UUID).
        /// </summary>
        /// <returns>Task with list of <see cref="IBluetoothRemoteGATTCharacteristic"/> result.</returns>
        Task<List<IBluetoothRemoteGATTCharacteristic>> GetCharacteristics();
    }
}
