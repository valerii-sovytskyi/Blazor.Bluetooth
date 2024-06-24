using System.Collections.ObjectModel;
using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class DeviceRequestComponent : BindableBase
{
    [Inject]
    public IBluetoothNavigator BluetoothNavigator { get; set; }

    private string _deviceName;
    public string DeviceName
    {
        get => _deviceName;
        set => SetProperty(ref _deviceName, value);
    }

    private string _deviceNamePrefix;
    public string DeviceNamePrefix
    {
        get => _deviceNamePrefix;
        set => SetProperty(ref _deviceNamePrefix, value);
    }
    
    private bool _allowAllDevices;
    public bool AllowAllDevices
    {
        get => _allowAllDevices;
        set => SetProperty(ref _allowAllDevices, value);
    }

    private string _serviceUuid;
    public string ServiceUuid
    {
        get => _serviceUuid;
        set => SetProperty(ref _serviceUuid, value);
    }
    
    [Parameter]
    public ObservableCollection<string> Logs { get; set; }

    [Parameter]
    public EventHandler<IDevice?> OnDeviceReceived { get; set; }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public async Task RequestDevice()
    {
        IsBusy = true;

        try
        {
            var isBleAvailable = await BluetoothNavigator.GetAvailability();
            if (!isBleAvailable)
            {
                Logs.Add("The BLE is not available on your browser");
            }

            var query = new RequestDeviceQuery
            {
                AcceptAllDevices = AllowAllDevices
            };

            if (!AllowAllDevices)
            {
                query.Filters = new List<Filter>
                {
                    new Filter
                    {
                        Name = string.IsNullOrWhiteSpace(DeviceName)
                            ? null
                            : DeviceName,
                        NamePrefix = string.IsNullOrWhiteSpace(DeviceNamePrefix)
                            ? null
                            : DeviceNamePrefix,
                    }
                };
            }

            if (!string.IsNullOrEmpty(ServiceUuid))
            {
                query.OptionalServices.Add(ServiceUuid);
            }

            var device = await BluetoothNavigator.RequestDevice(query);

            OnDeviceReceived.Invoke(this, device);
        }
        catch (System.Exception ex)
        {
            Logs.Add($"Exception: {ex.Message}");
        }

        IsBusy = false;
    }
}