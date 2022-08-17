using System.Collections.Generic;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Bluetooth service data map interface.
    /// </summary>
    public interface IBluetoothServiceDataMap : IDictionary<string, byte[]> 
    {
    }
}
