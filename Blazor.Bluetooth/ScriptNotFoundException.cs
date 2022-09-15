using System;

namespace Blazor.Bluetooth
{
    public class ScriptNotFoundException : Exception
    {
        public ScriptNotFoundException(Exception ex)
            : base("JSInteropt is not found, check if you added <script src=\"_content/Blazor.Bluetooth/JSInterop.js\"></script> in your wwwrooot/index.html for Client App, or in _Host.cshtml (.net5) /  _Layout.cshtml (.net6) for Server App.", ex)
        {
        }
    }
}
