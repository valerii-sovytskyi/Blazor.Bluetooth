using System.Collections.ObjectModel;
using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class DeviceRequestComponent : BindableBase
{
    [Inject]
    public IBluetoothNavigator BluetoothNavigator { get; set; }

    private bool _addFilter;
    public bool AddFilter
    {
        get => _addFilter;
        set => SetProperty(ref _addFilter, value);
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

    public RequestDeviceOptions Options = new RequestDeviceOptions();

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

            var device = await BluetoothNavigator.RequestDevice(Options);

            OnDeviceReceived.Invoke(this, device);
        }
        catch (Exception ex)
        {
            Logs.Add($"Exception: {ex.Message}");
        }

        IsBusy = false;
    }
}