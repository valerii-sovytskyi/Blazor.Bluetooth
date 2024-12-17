using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Represents a GATT Server on a remote device.
    /// </summary>
    public interface IBluetoothRemoteGATTServer
    {
        /// <summary>
        /// Gets a string that uniquely identifies a device.
        /// </summary>
        string DeviceUuid { get; }

        /// <summary>
        /// Gets a boolean value that returns true while this script execution environment is connected to this.device. It can be false while the user agent is physically connected.
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// Causes the script execution environment to connect to device.
        /// </summary>
        /// <returns>Task.</returns>
        Task Connect();

        /// <summary>
        /// Causes the script execution environment to disconnect from device.
        /// </summary>
        /// <returns>Task.</returns>
        Task Disconnect();

        /// <summary>
        /// Returns the primary <see cref="IBluetoothRemoteGATTService"/> offered by the bluetooth device for a specified BluetoothServiceUUID.
        /// </summary>
        /// <param name="uuid">A Bluetooth service universally unique identifier for a specified device.</param>
        /// <returns>Task with <see cref="IBluetoothRemoteGATTService"/> result.</returns>
        Task<IBluetoothRemoteGATTService> GetPrimaryService(string uuid);

        /// <summary>
        /// Returns a list of primary <see cref="IBluetoothRemoteGATTService"/> objects offered by the bluetooth device for a specified BluetoothServiceUUID.
        /// </summary>
        /// <param name="uuid">A Bluetooth service universally unique identifier for a specified device.</param>
        /// <returns>Task with list of <see cref="IBluetoothRemoteGATTService"/> result.</returns>
        Task<List<IBluetoothRemoteGATTService>> GetPrimaryServices(string uuid);

        /// <summary>
        /// Get current device and check Connected property, made to check the state in runtime, because Connected property is actually setting by Connect/Disconnect/GetConnected only.
        /// </summary>
        /// <returns>Task with is connected result.</returns>
        Task<bool> GetConnected();
    }
}
