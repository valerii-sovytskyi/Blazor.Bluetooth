using System.Collections.ObjectModel;
using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class DeviceComponent : BindableBase
{
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
    
    [Parameter]
    public IDevice Device { get; set; }
    
    [Parameter]
    public ObservableCollection<string> Logs { get; set; }
    
    public async Task ConnectDevice()
    {
        IsBusy = true;

        try
        {
            await Device.Gatt.Connect();

            StateHasChanged();
        }
        catch (System.Exception ex)
        {
            Logs.Add($"Exception: {ex.Message}");
        }

        IsBusy = false;
    }

    public async Task DisconnectDevice()
    {
        IsBusy = true;

        try
        {
            await Device.Gatt.Disonnect();
            StateHasChanged();
        }
        catch (System.Exception ex)
        {
            Logs.Add($"Exception: {ex.Message}");
        }

        if (!await Device.Gatt.GetConnected())
        {
            Logs.Add("Info: " + Device.Name + " disconnected" );
        }

        IsBusy = false;
    }

    public async Task UpdateIsConnected()
    {
        IsBusy = true;

        try
        {
            await Device.Gatt.GetConnected();
            StateHasChanged();
        }
        catch (System.Exception ex)
        {
            Logs.Add($"Exception: {ex.Message}");
        }

        IsBusy = false;
    }
}
