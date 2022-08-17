using System;

namespace Blazor.Bluetooth
{
    public interface IBluetoothAdvertisingEvent
    {
        /// <summary>
        /// Gets appearance.
        /// </summary>
        public ushort Appearance { get; }

        /// <summary>
        /// Gets device.
        /// </summary>
        public IDevice Device { get; }

        /// <summary>
        /// Gets manufacturer data.
        /// </summary>
        public IBluetoothManufacturerDataMap ManufacturerData { get; }

        /// <summary>
        /// Gets name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets RSSI.
        /// </summary>
        public sbyte Rssi { get; }

        /// <summary>
        /// Gets service data.
        /// </summary>
        public IBluetoothServiceDataMap ServiceData { get; }

        /// <summary>
        /// Gets TX power.
        /// </summary>
        public sbyte TxPower { get; }

        /// <summary>
        /// Gets UUIDs.
        /// </summary>
        public string[] Uuids { get; }
    }
}
