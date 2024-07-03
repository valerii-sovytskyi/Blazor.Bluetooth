using System.Collections.ObjectModel;
using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class BluetoothUuidsComponent : BindableBase
{
    [Inject]
    public IBluetoothNavigator BluetoothNavigator { get; set; }
    
    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _UUID;
    public string UUID
    {
        get => _UUID;
        set => SetProperty(ref _UUID, value);
    }
    
    [Parameter]
    public ObservableCollection<string> Logs { get; set; }

    public async Task SetGetService()
    {
        try
        {
            UUID = await BluetoothUUID.GetService(Name);
        }
        catch (Exception ex)
        {
            Logs.Add(ex.Message);
        }
    }
    
    public async Task SetGetCharacteristic()
    {
        try
        {
            UUID = await BluetoothUUID.GetCharacteristic(Name);
        }
        catch (Exception ex)
        {
            Logs.Add(ex.Message);
        }
    }
    
    public async Task SetGetDescriptor()
    {
        try
        {
            UUID = await BluetoothUUID.GetDescriptor(Name);
        }
        catch (Exception ex)
        {
            Logs.Add(ex.Message);
        }
    }
    
    public async Task SetGetCanonical()
    {
        try
        {
            UUID = await BluetoothUUID.GetCanonicalUUID(Name);
        }
        catch (Exception ex)
        {
            Logs.Add(ex.Message);
        }
    }
}
