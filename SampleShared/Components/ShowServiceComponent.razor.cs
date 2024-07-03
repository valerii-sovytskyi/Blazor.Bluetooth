using System.Collections.ObjectModel;
using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class ShowServiceComponent : BindableBase
{
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
    
    private string _characteristicUUID;
    public string CharacteristicUUID
    {
        get => _characteristicUUID;
        set => SetProperty(ref _characteristicUUID, value);
    }

    public ObservableCollection<IBluetoothRemoteGATTCharacteristic> Characteristics { get; set; } = new();

    [Parameter]
    public IBluetoothRemoteGATTService Service { get; set; }
    
    [Parameter]
    public ObservableCollection<string> Logs { get; set; }
    
    public async Task OnGetCharacteristicByUUIDClicked()
    {
        if (string.IsNullOrEmpty(CharacteristicUUID))
        {
            return;
        }

        try
        {
            var characteristic = await Service.GetCharacteristic(CharacteristicUUID);
            var existing = Characteristics.FirstOrDefault(x => x.Uuid == CharacteristicUUID);
            if (existing != null)
            {
                Characteristics.Remove(existing);
            }

            Characteristics.Add(characteristic);
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }

    public async Task OnGetCharacteristicsByUUIDClicked()
    {
        if (string.IsNullOrEmpty(CharacteristicUUID))
        {
            return;
        }

        try
        {
            var characteristics = await Service.GetCharacteristics(CharacteristicUUID);
            var existing = Characteristics.Where(x => x.Uuid == CharacteristicUUID);
            if (existing != null)
            {
                foreach (var item in existing)
                {
                    Characteristics.Remove(item);
                }
            }

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
}