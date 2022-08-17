using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// The Web Bluetooth API provides the ability to connect and interact with Bluetooth Low Energy peripherals.
    /// </summary>
    public interface IBluetoothNavigator
    {
        /// <summary>
        /// An event handler that runs when an event of type <see cref="OnAvailabilityChanged"/> has fired.
        /// </summary>
        event Action OnAvailabilityChanged;

        /// <summary>
        /// Returns a reference to the device, if any, from which the user opened the current page. For example, an Eddystone beacon might advertise a URL, which the user agent allows the user to open. A <see cref="Device"/> representing the beacon would be available through <see cref="ReferringDevice"/>.
        /// </summary>
        /// <returns>Task with a device.</returns>
        Task<IDevice> ReferringDevice();

        /// <summary>
        /// Returns a boolean value indicating whether the user-agent has the ability to support Bluetooth. Some user-agents let the user configure an option that affects what is returned by this value. If this option is set, that is the value returned by this method.
        /// </summary>
        /// <returns>Task with availability result.</returns>
        Task<bool> GetAvailability();

        /// <summary>
        /// Returns a list of <see cref="Device"/> which the origin already obtained permission for via a call to <see cref="RequestDevice(RequestDeviceQuery)"/>.
        /// </summary>
        /// <returns>Task with a list of devices result.</returns>
        Task<List<IDevice>> GetDevices();

        /// <summary>
        /// Request from the user a device. If there is no chooser UI, this method returns the first device matching the criteria.
        /// </summary>
        /// <param name="query">A filter query.</param>
        /// <returns>Task with a device result.</returns>
        /// <exception cref="RequestDeviceCancelledException">Exception thrown in case user cancel connecting to the device.</exception>
        Task<IDevice> RequestDevice(RequestDeviceQuery query);
    }
}
