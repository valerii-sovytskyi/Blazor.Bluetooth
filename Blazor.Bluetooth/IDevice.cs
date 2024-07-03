using System;
using System.Threading.Tasks;

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

        /// <summary>
        /// On advertisement received event.
        /// </summary>
        event Action<IBluetoothAdvertisingEvent> OnAdvertisementReceived;

        /// <summary>
        /// Activate receiving advertisements from the device.
        /// Advertisements will stop receiving once you connect to the device.
        /// </summary>
        /// <exception cref="AdvertisementsUnavailableException">Will throw in case of Experimental mode inactive.</exception>
        Task WatchAdvertisements();

        /// <summary>
        /// Provides a way for the page to revoke access to a device the user has granted access to.
        /// </summary>
        Task Forget();
    }
}
