using System.Collections.Generic;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Bluetooth manufacturer map interface.
    /// </summary>
    public interface IBluetoothManufacturerDataMap : IDictionary<string, byte[]>
    {
    }
}
