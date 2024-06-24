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
    
    private string _serviceUUID;
    public string ServiceUUID
    {
        get => _serviceUUID;
        set => SetProperty(ref _serviceUUID, value);
    }

    private string _characteristicUUID;
    public string CharacteristicUUID
    {
        get => _characteristicUUID;
        set => SetProperty(ref _characteristicUUID, value);
    }
        
    private string _descriptorUUID;
    public string DescriptorUUID
    {
        get => _descriptorUUID;
        set => SetProperty(ref _descriptorUUID, value);
    }
    
    public ObservableCollection<IBluetoothRemoteGATTService> Services { get; set; } =
        new ObservableCollection<IBluetoothRemoteGATTService>();

    public ObservableCollection<IBluetoothRemoteGATTCharacteristic> Characteristics { get; set; } =
        new ObservableCollection<IBluetoothRemoteGATTCharacteristic>();

    public ObservableCollection<IBluetoothRemoteGATTDescriptor> Descriptors { get; set; } =
        new ObservableCollection<IBluetoothRemoteGATTDescriptor>();

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

            Services.Clear();
            ServiceUUID = null;
            CharacteristicUUID = null;
            
            StateHasChanged();
        }
        catch (System.Exception ex)
        {
            Logs.Add($"Exception: {ex.Message}");
        }

        IsBusy = false;
    }

    public async Task OnGetServiceByUUIDClicked()
    {
        if (string.IsNullOrEmpty(ServiceUUID))
        {
            return;
        }

        Characteristics.Clear();
        Descriptors.Clear();
        CharacteristicUUID = null;
        DescriptorUUID = null;

        try
        {
            var service = await Device.Gatt.GetPrimaryService(ServiceUUID);
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

        Characteristics.Clear();
        Descriptors.Clear();
        CharacteristicUUID = null;
        DescriptorUUID = null;

        try
        {
            var services = await Device.Gatt.GetPrimaryServices(ServiceUUID);
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

    public async Task OnGetCharacteristicByUUIDClicked(IBluetoothRemoteGATTService service)
    {
        if (string.IsNullOrEmpty(CharacteristicUUID))
        {
            return;
        }

        Descriptors.Clear();
        DescriptorUUID = null;

        try
        {
            var characteristic = await service.GetCharacteristic(CharacteristicUUID);
            Characteristics.Add(characteristic);
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }

    public async Task OnGetCharacteristicsByUUIDClicked(IBluetoothRemoteGATTService service)
    {
        if (string.IsNullOrEmpty(CharacteristicUUID))
        {
            return;
        }

        Descriptors.Clear();
        DescriptorUUID = null;

        try
        {
            var characteristics = await service.GetCharacteristics(CharacteristicUUID);
            foreach (var characteristic in characteristics)
            {
                Characteristics.Add(characteristic);
            }
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }

    public async Task OnGetDescriptorByUUIDClicked(IBluetoothRemoteGATTCharacteristic characteristic)
    {
        if (string.IsNullOrEmpty(DescriptorUUID))
        {
            return;
        }

        try
        {
            var descriptor = await characteristic.GetDescriptor(DescriptorUUID);
            Descriptors.Add(descriptor);
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }

    public async Task OnGetDescriptorsByUUIDClicked(IBluetoothRemoteGATTCharacteristic characteristic)
    {
        if (string.IsNullOrEmpty(DescriptorUUID))
        {
            return;
        }

        try
        {
            var descriptors = await characteristic.GetDescriptors(DescriptorUUID);
            foreach (var descriptor in descriptors)
            {
                Descriptors.Add(descriptor);
            }
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
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
