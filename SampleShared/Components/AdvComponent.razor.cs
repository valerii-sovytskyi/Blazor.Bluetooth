using System.Collections.ObjectModel;
using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class AdvComponent : BindableBase
{
    private bool _advertisementsReceiveActivated;
    public bool AdvertisementsReceiveActivated
    {
        get => _advertisementsReceiveActivated;
        set => SetProperty(ref _advertisementsReceiveActivated, value);
    }

    private bool _advertisementsFeatureDissabled;
    public bool AdvertisementsFeatureDissabled
    {
        get => _advertisementsFeatureDissabled;
        set => SetProperty(ref _advertisementsFeatureDissabled, value);
    }
    
    private IBluetoothAdvertisingEvent _bluetoothAdvertisingEvent;
    public IBluetoothAdvertisingEvent BluetoothAdvertisingEvent
    {
        get => _bluetoothAdvertisingEvent;
        set => SetProperty(ref _bluetoothAdvertisingEvent, value);
    }

    private IDevice _device;
    [Parameter]
    public IDevice Device
    {
        get => _device;
        set
        {
            if (_device != value)
            {
                _advertisementsReceiveActivated = false;
                _advertisementsFeatureDissabled = false;
                _device = value;
            }
        }
    }
    
    [Parameter]
    public ObservableCollection<string> Logs { get; set; }

    public async Task StartReceivingAdvertisements()
    {
        try
        {
            Device.OnAdvertisementReceived += Device_OnAdvertisementReceived;
            await Device.WatchAdvertisements();
            AdvertisementsReceiveActivated = true;
        }
        catch (AdvertisementsUnavailableException ex)
        {
            AdvertisementsFeatureDissabled = true;
            Logs.Add($"AdvertisementsUnavailableException: {ex.Message}");
        }
    }
    
    private void Device_OnAdvertisementReceived(IBluetoothAdvertisingEvent bluetoothAdvertisingEvent)
    {
        Logs.Add("Add received at " + DateTime.Now.ToString("hh:mm:ss"));
        BluetoothAdvertisingEvent = bluetoothAdvertisingEvent;
    }
}
