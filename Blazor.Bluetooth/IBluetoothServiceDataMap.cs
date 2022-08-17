using System;
using System.Collections.Generic;

namespace Blazor.Bluetooth
{
    /// <summary>
    /// Bluetooth service data map interface.
    /// </summary>
    public interface IBluetoothServiceDataMap : IDictionary<object, byte[]> // TODO: Not sure if this is the right type, and Key can be more specific.
    {
    }
}
