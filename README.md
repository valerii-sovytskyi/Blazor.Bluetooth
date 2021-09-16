# How to use Blazor.Bluetooth

Blazor.Bluetooth makes it easy to connect Blazor to your Bluetooth devices using Web Bluetooth.

Works both Client-side and Server-side.

## Getting Started

1. Add Nuget package Blazor.Bluetooth
2. In Program.cs add ```builder.Services.AddBluetoothNavigator();```
3. In your wwwrooot/index.html add
```<script src="_content/Blazor.Bluetooth/JSInterop.js"></script>```
4. In the component you want to connect to a device add the Blazor.Bluetooth Namespace
```@using Blazor.Bluetooth```
5. Inject the IBluetoothNavigator (the instance that will communicate with your device)
```@inject IBluetoothNavigator navigator```

Please feel free to add ideas / problems / needs you might have or make a PR.
