# How to use Blazor.Bluetooth

Blazor.Bluetooth makes it easy to connect Blazor to your Bluetooth devices using Web Bluetooth.

Works both **Client-side** and **Server-side**.

## Getting Started

1. Add Nuget package [Blazor.Bluetooth](https://www.nuget.org/packages/Blazor.Bluetooth)
2. In Program.cs add ```builder.Services.AddBluetoothNavigator();```
3. Add JSInterop.js to the project

- For the **Client**: In your **wwwrooot/index.html** add ```<script src="_content/Blazor.Bluetooth/JSInterop.js"></script>```
- For the **Server**: In your _Host.cshtml for .**net5** and _Layout.cshtml for **.net6** add ```<script src="_content/Blazor.Bluetooth/JSInterop.js"></script>```

4. In the component you want to connect to a device add the Blazor.Bluetooth Namespace ```@using Blazor.Bluetooth``` or add it to your ```_Imports.razor```
5. Inject the **IBluetoothNavigator** (the instance that will communicate with your device) ```@inject IBluetoothNavigator navigator```

You can check and run [SampleClientSide](https://github.com/valerii-sovytskyi/Blazor.Bluetooth/tree/master/SampleClientSide) to test device connection.
But right now let's see what we can do here

## How to use

### Bluetooth

First of all you can check if user's browser support Bluetooth connection by calling **IBluetoothNavigator.GetAvailability**

### Request a device

Create a **RequestDeviceQuery** with all the options you would like to get a device. But note that it's important if **AcceptAllDevices** is **true**, then do not set **Filters**, other ways you have to fill **Filters**.

```
var query = new RequestDeviceQuery { AcceptAllDevices = true };
var device = await BluetoothNavigator.RequestDevice(query);
```

### Connect/Disconnect to the device

Connect to the device.

```await device.Gatt.Connect();```

_Note: that for my case I want to able to connect to the device all the time, sometimes it took like 5 times to connect, so it would be nice you to implement some service to run connection for few times untill you will be connected or time out. It you failed to connect, you will get an **Exception**_

Disconnect from the device will raise **IDevice.OnGattServerDisconnected** event.

```await Device.Gatt.Disonnect();```

_Note: Sometimes to reconnect to the device you will have to wait, it dependec on a device but for my case I needed to wait up to 3 sec. ```await Task.Delay(3000);```_

### Get characteristic to read/write and get notifications

To read or write a byte array you have to get characteristic, (or descriptor it dependes). Also you can check if your characteristic support write or read.

```
var service = await device.Gatt.GetPrimaryService("Your service UUID");
var characteristic = await service.GetCharacteristic("Your characteristic UUID");
if (characteristic.Properties.Write)
{
  characteristic.OnRaiseCharacteristicValueChanged += (sender, e) => { };
  await characteristic.StartNotifications();
  await characteristic.WriteValueWithResponse(/* Your byte array */);
}
```

_Note: do not forget to unsubscribe from the event and call StopNotifications._

### Others

I didn't describe all the functionality, some of them I didn't have possibility to test so there could be some issues and I would like to hear from you a feedback to make this nuget fully work.

## Release notes

- [1.0.2](https://www.nuget.org/packages/Blazor.Bluetooth/1.0.2)
1. IBluetoothRemoteGATTServer has a new implementation to GetConnected
 
_Why? It means you will check if device connected on runtime. Because the property Connect will be updated only by your actions Connect/Disconnect/GetConnected, so we need to actually check if Connected still connected to device, this method can help. Consider of using this method instead. One more think, we have a bug related to after Connect we not always connected successfully, but Connect is true, but after some time, we receive exceptions as device is disconnected. Consider to connect to device, wait some time, up to 1 sec I guess, then check if you are connected by calling Connect method. I didn't implement this functionality inside Blazor.Bluetooth, because this is only a mirror for real web bluetooth, so this is not a part of real lib._

- [1.0.3](https://www.nuget.org/packages/Blazor.Bluetooth/1.0.3)
1. Added RequestDeviceCancelledException - by handling this exception user can just inore it, as it indicate, user clicked on cancel button.
2. Removed Newtonsoft.Json as we can use System.Text.Json
3. Filter.Services / RequestDeviceQuery.Filters / RequestDeviceQuery.OptionalServices are null by default, and removed useless methods ShouldSerialize_.

- [1.0.4](https://www.nuget.org/packages/Blazor.Bluetooth/1.0.4)
1. Fixed issue if user try to connect to one device, then disconnect, then try to connect to another device.
2. Fixed issue with Paired bluetooth devices list, it cause issue if you trying to talk to device if you reconnected to the same device.

- [1.0.5](https://www.nuget.org/packages/Blazor.Bluetooth/1.0.5)
1. Added Watch advertisements to the IDevice
2. Updated tests, also uploaded tester so you can test your device without prepearing IDE.
3. Fixed all event subscribtions to run synchronously, because it was run method faster then event was subscribet inside JSInteropt.js.

- [1.0.5.5](https://www.nuget.org/packages/Blazor.Bluetooth/1.0.5.5)
1. Fixed critical issue #1 to this library on Blazor Server App.

- [1.0.5.6](https://www.nuget.org/packages/Blazor.Bluetooth/1.0.5.6)
1. Fixed issue with connecting only for Current device instead of connecting directly by the device ID
2. Updated sample for testing bluetooth.getAvailability and bluetooth.getDevices.

- [1.0.6.0](https://www.nuget.org/packages/Blazor.Bluetooth/1.0.6.0)

1. Fixed reading Characteristic properties.
2. Add support for net8. 

## Additionaly

- Some features are working only with Experimental feature activated, for example input this reference into your browser (chrome/edge) **about:flags/#enable-web-bluetooth-new-permissions-backend** and enable it. Or more general option **about:flags/#enable-experimental-web-platform-features**.
- Try [Blazor.Bluetooth Web tester](https://blazorbluetooth.azurewebsites.net/), so you do not have to run the code