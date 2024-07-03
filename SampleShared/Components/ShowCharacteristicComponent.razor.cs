using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace SampleShared.Components;
public partial class ShowCharacteristicComponent : BindableBase
{
    public ObservableCollection<IBluetoothRemoteGATTDescriptor> Descriptors { get; set; } = new();
            
    private string _descriptorUUID;
    public string DescriptorUUID
    {
        get => _descriptorUUID;
        set => SetProperty(ref _descriptorUUID, value);
    }
    
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
    
    [Parameter]
    public IBluetoothRemoteGATTCharacteristic Characteristic { get; set; }

    [Parameter]
    public ObservableCollection<string> Logs { get; set; }

    public async Task OnGetDescriptorByUUIDClicked(IBluetoothRemoteGATTCharacteristic characteristic)
    {
        if (string.IsNullOrEmpty(DescriptorUUID))
        {
            return;
        }

        try
        {
            var descriptor = await characteristic.GetDescriptor(DescriptorUUID);
            var existing = Descriptors.FirstOrDefault(x => x.Uuid == DescriptorUUID);
            if (existing != null)
            {
                Descriptors.Remove(existing);
            }

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
            var existing = Descriptors.Where(x => x.Uuid == DescriptorUUID);
            if (existing != null)
            {
                foreach (var item in existing)
                {
                    Descriptors.Remove(item);
                }
            }

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
}
