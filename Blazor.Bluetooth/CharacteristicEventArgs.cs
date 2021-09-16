using System;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Characteristic event args for <see cref="IBluetoothRemoteGATTCharacteristic.OnRaiseCharacteristicValueChanged"/>.
    /// </summary>
    public class CharacteristicEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a string representing the UUID of this service.
        /// </summary>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Gets a string containing the UUID of the characteristic, for example '00002a37-0000-1000-8000-00805f9b34fb' for the Heart Rate Measurement characteristic.
        /// </summary>
        public Guid CharacteristicId { get; set; }

        /// <summary>
        /// Gets a value bytes.
        /// </summary>
        public byte[] Value { get; set; }
    }
}
