using System.Collections.ObjectModel;
using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class BleTesterPageComponent : BindableBase
{
    [Inject]
    public IBluetoothNavigator BluetoothNavigator { get; set; }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public ObservableCollection<string> Logs { get; set; } = new ObservableCollection<string>();

    public ObservableCollection<IDevice> Devices { get; set; } =
        new ObservableCollection<IDevice>();

    public BleTesterPageComponent()
    {
        Logs.CollectionChanged += (o, e) => StateHasChanged();
    }

    public async Task RemoveDevice(IDevice device)
    {
        if (device.Gatt.Connected)
        {
            await device.Gatt.Disonnect();
        }

        Devices.Remove(device);
        StateHasChanged();
    }

    public async Task OnGetDeviceClicked()
    {
        try
        {
            var devices = await BluetoothNavigator.GetDevices();
            if (devices != null)
            {
                Logs.Add($"Just got {devices.Count} devices");

                foreach (var device in devices)
                {
                    var existingDevice = Devices.FirstOrDefault(x => x.Id == device.Id);
                    if (existingDevice != null)
                    {
                        Devices.Remove(existingDevice);
                    }

                    Devices.Add(device);
                    StateHasChanged();
                }
            }
        }
        catch (System.Exception ex)
        {
            Logs.Add($"Exception: {ex.Message}");
        }
    }

    private void OnDeviceReceived(object sender, IDevice? device)
    {
        if (device is null)
        {
            return;
        }

        var existingDevice = Devices.FirstOrDefault(x => x.Id == device.Id);
        if (existingDevice != null)
        {
            Devices.Remove(existingDevice);
        }

        Devices.Add(device);
        StateHasChanged();
    }

    public async Task OnGetAvailabilityClicked()
    {
        try
        {
            BluetoothNavigator.OnAvailabilityChanged -= BluetoothNavigator_OnAvailabilityChanged;
            BluetoothNavigator.OnAvailabilityChanged += BluetoothNavigator_OnAvailabilityChanged;
            var isAvailable = await BluetoothNavigator.GetAvailability();
            var txt = isAvailable ? "Bluetooth adapter available" : "Bluetooth adapter is not available";
            Logs.Add(txt);
        }
        catch (System.Exception ex)
        {
            Logs.Add($"Exception: {ex.Message}");
        }
    }

    private void BluetoothNavigator_OnAvailabilityChanged()
    {
        Logs.Add($"BluetoothNavigator_OnAvailabilityChanged called");
    }
}