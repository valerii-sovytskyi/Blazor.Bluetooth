using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace SampleShared.Components;
public partial class ShowConnectedDeviceComponent : BindableBase
{
    private string _serviceUUID;
    public string ServiceUUID
    {
        get => _serviceUUID;
        set => SetProperty(ref _serviceUUID, value);
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
    
    public ObservableCollection<IBluetoothRemoteGATTService> Services { get; set; } = new();

    [Parameter]
    public IDevice Device { get; set; }

    [Parameter]
    public ObservableCollection<string> Logs { get; set; }

    public async Task OnGetServiceByUUIDClicked()
    {
        if (string.IsNullOrEmpty(ServiceUUID))
        {
            return;
        }

        try
        {
            var service = await Device.Gatt.GetPrimaryService(ServiceUUID);
            var existing = Services.FirstOrDefault(x => x.Uuid == ServiceUUID);
            if (existing != null)
            {
                Services.Remove(existing);
            }

            Services.Add(service);
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }

    public async Task OnGetServicesByUUIDClicked()
    {
        if (string.IsNullOrEmpty(ServiceUUID))
        {
            return;
        }

        try
        {
            var services = await Device.Gatt.GetPrimaryServices(ServiceUUID);
            var existing = Services.Where(x => x.Uuid == ServiceUUID);
            if (existing != null)
            {
                foreach (var item in existing)
                {
                    Services.Remove(item);
                }
            }

            foreach (var service in services)
            {
                Services.Add(service);
            }
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }
}