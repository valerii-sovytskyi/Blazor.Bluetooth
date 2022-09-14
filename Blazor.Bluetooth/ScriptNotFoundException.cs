using System;

namespace Blazor.Bluetooth
{
    public class ScriptNotFoundException : Exception
    {
        public ScriptNotFoundException(Exception ex)
            : base("JSInteropt.js not found, please refer to the https://github.com/valerii-sovytskyi/Blazor.Bluetooth/blob/master/README.md for more details.", ex)
        {
        }
    }
}
