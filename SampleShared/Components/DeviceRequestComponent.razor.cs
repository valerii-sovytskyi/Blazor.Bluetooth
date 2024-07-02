using System.Collections.ObjectModel;
using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class DeviceRequestComponent : BindableBase
{
    [Inject]
    public IBluetoothNavigator BluetoothNavigator { get; set; }

    #region Filter

    private string _serviceUuid;
    public string ServiceUuid
    {
        get => _serviceUuid;
        set => SetProperty(ref _serviceUuid, value);
    }
    
    #endregion

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
        catch (System.Exception ex)
        {
            Logs.Add($"Exception: {ex.Message}");
        }

        IsBusy = false;
    }

    private void AddNewFilter()
    {
        if (Options.Filters is null)
        {
            Options.Filters = new List<Filter>();
        }

        Options.Filters.Add(new Filter());
        StateHasChanged();
    }

    private void RemoveFilter(Filter filter)
    {
        Options.Filters.Remove(filter);

        if (Options.Filters.Count == 0)
        {
            Options.Filters = null;
        }
        StateHasChanged();
    }

    private void AddManufacturerData()
    {
        if (Options.OptionalManufacturerData is null)
        {
            Options.OptionalManufacturerData = new List<ManufacturerData>();
        }

        Options.OptionalManufacturerData.Add(new ManufacturerData());
        StateHasChanged();
    }
    
    private void RemoveManufacturerData(ManufacturerData manufacturerData)
    {
        Options.OptionalManufacturerData.Remove(manufacturerData);
        if (Options.OptionalManufacturerData.Count == 0)
        {
            Options.OptionalManufacturerData = null;
        }

        StateHasChanged();
    }
    
    private void AddManufacturerData(Filter filter)
    {
        if (filter.ManufacturerData is null)
        {
            filter.ManufacturerData = new List<ManufacturerData>();
        }

        filter.ManufacturerData.Add(new ManufacturerData());
        StateHasChanged();
    }
    
    private void RemoveManufacturerData(Filter filter, ManufacturerData manufacturerData)
    {
        filter.ManufacturerData.Remove(manufacturerData);
        if (filter.ManufacturerData.Count == 0)
        {
            filter.ManufacturerData = null;
        }

        StateHasChanged();
    }

    private void AddService()
    {
        if (Options.OptionalServices is null)
        {
            Options.OptionalServices = new List<string>();
        }

        Options.OptionalServices.Add(string.Empty);
        StateHasChanged();
    }
    
    private void RemoveService(int index)
    {
        Options.OptionalServices.RemoveAt(index);
        if (Options.OptionalServices.Count == 0)
        {
            Options.OptionalServices = null;
        }

        StateHasChanged();
    }

    private void OnServiceTextChanged(int serviceIndex, object arg)
    {
        Options.OptionalServices[serviceIndex] = arg.ToString();
    }

    private void AddService(Filter filter)
    {
        if (filter.Services is null)
        {
            filter.Services = new List<string>();
        }

        filter.Services.Add(string.Empty);
        StateHasChanged();
    }
    
    private void RemoveService(Filter filter, int index)
    {
        filter.Services.RemoveAt(index);
        if (filter.Services.Count == 0)
        {
            filter.Services = null;
        }

        StateHasChanged();
    }

    private void OnServiceTextChanged(Filter filter, int serviceIndex, object arg)
    {
        filter.Services[serviceIndex] = arg.ToString();
    }
}