using System;

namespace Blazor.Bluetooth
{
    public interface IDevice
    {
        /// <summary>
        /// Gets a string that uniquely identifies a device.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets a string that provices a human-readable name for the device.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a reference to the device's BluetoothRemoteGATTServer.
        /// </summary>
        IBluetoothRemoteGATTServer Gatt { get; }

        /// <summary>
        /// On GATT server disconnected event represent disconnection from the device.
        /// </summary>
        event Action OnGattServerDisconnected;
    }
}
