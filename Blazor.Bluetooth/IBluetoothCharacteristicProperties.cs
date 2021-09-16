namespace Blazor.Bluetooth
{
    /// <summary>
    /// Provides the operations that are valid on the given BluetoothRemoteGATTCharacteristic.
    /// </summary>
    public interface IBluetoothCharacteristicProperties
    {
        /// <summary>
        /// Gets a boolean that is true if signed writing to the characteristic value is permitted.
        /// </summary>
        bool AuthenticatedSignedWrites { get; }

        /// <summary>
        /// Return a boolean that is true if the broadcast of the characteristic value is permitted using the Server Characteristic Configuration Descriptor.
        /// </summary>
        bool Broadcast { get; }

        /// <summary>
        /// Return a boolean that is true if indications of the characteristic value with acknowledgement is permitted.
        /// </summary>
        bool Indicate { get; }

        /// <summary>
        /// Return a boolean that is true if notifications of the characteristic value without acknowledgement is permitted.
        /// </summary>
        bool Notify { get; }

        /// <summary>
        /// Return a boolean that is true if the reading of the characteristic value is permitted.
        /// </summary>
        bool Read { get; }

        /// <summary>
        /// Return a boolean that is true if reliable writes to the characteristic is permitted.
        /// </summary>
        bool ReliableWrite { get; }

        /// <summary>
        /// Return a boolean that is true if reliable writes to the characteristic descriptor is permitted.
        /// </summary>
        bool WritableAuxiliaries { get; }

        /// <summary>
        /// Return a boolean that is true if the writing to the characteristic with response is permitted.
        /// </summary>
        bool Write { get; }

        /// <summary>
        /// Return a boolean that is true if the writing to the characteristic without response is permitted.
        /// </summary>
        bool WriteWithoutResponse { get; }
    }
}
