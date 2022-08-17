namespace Blazor.Bluetooth
{
    internal class BluetoothAdvertisingEvent : IBluetoothAdvertisingEvent
    {
        #region Internal fields

        public ushort InternalAppearance { get; set; }
        public Device InternalDevice { get; set; }
        public BluetoothManufacturerDataMap InternalManufacturerData { get; set; }
        public string InternalName { get; set; }
        public sbyte InternalRssi { get; set; }
        public BluetoothServiceDataMap InternalServiceData { get; set; }
        public sbyte InternalTxPower { get; set; }

        // TODO: Originaly this is Array type, but I don't know if this is array of strings. Find out the type.
        public string[] InternalUuids { get; set; }

        #endregion

        #region Public fields

        public ushort Appearance => InternalAppearance;

        public IDevice Device => InternalDevice;

        public IBluetoothManufacturerDataMap ManufacturerData => InternalManufacturerData;

        public string Name => InternalName;

        public sbyte Rssi => InternalRssi;

        public IBluetoothServiceDataMap ServiceData => InternalServiceData;

        public sbyte TxPower => InternalTxPower;

        public string[] Uuids => InternalUuids;

        #endregion
    }
}
