# How to use Blazor.Bluetooth

Blazor.Bluetooth makes it easy to connect Blazor to your Bluetooth devices using Web Bluetooth.

Works both **Client-side** and **Server-side**.

## Getting Started

1. Add Nuget package [Blazor.Bluetooth](https://www.nuget.org/packages/Blazor.Bluetooth)
2. In Program.cs add ```builder.Services.AddBluetoothNavigator();```
3. In your **wwwrooot/index.html** add ```<script src="_content/Blazor.Bluetooth/JSInterop.js"></script>```
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
