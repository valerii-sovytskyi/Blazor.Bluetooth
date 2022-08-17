using System.Collections.Generic;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Bluetooth manufacturer map interface.
    /// </summary>
    public interface IBluetoothManufacturerDataMap : IDictionary<object, byte[]> // TODO: Not sure if this is the right type, and Key can be more specific.
    {
    }
}
