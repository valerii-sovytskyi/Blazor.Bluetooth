namespace Blazor.Bluetooth
{
    internal class BluetoothCharacteristicProperties : IBluetoothCharacteristicProperties
    {
        #region Internal fields

        public bool InternalAuthenticatedSignedWrites { get; set; }

        public bool InternalBroadcast { get; set; }

        public bool InternalIndicate { get; set; }

        public bool InternalNotify { get; set; }

        public bool InternalRead { get; set; }

        public bool InternalReliableWrite { get; set; }

        public bool InternalWritableAuxiliaries { get; set; }

        public bool InternalWrite { get; set; }

        public bool InternalWriteWithoutResponse { get; set; }

        #endregion

        #region Public fields

        public bool AuthenticatedSignedWrites => InternalAuthenticatedSignedWrites;

        public bool Broadcast => InternalBroadcast;

        public bool Indicate => InternalIndicate;

        public bool Notify => InternalNotify;

        public bool Read => InternalRead;

        public bool ReliableWrite => InternalReliableWrite;

        public bool WritableAuxiliaries => InternalWritableAuxiliaries;

        public bool Write => InternalWrite;

        public bool WriteWithoutResponse => InternalWriteWithoutResponse;

        #endregion
    }
}
