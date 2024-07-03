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
    
    private string _readRead;
    public string ValueRead
    {
        get => _readRead;
        set => SetProperty(ref _readRead, value);
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
    
    private bool _isNotificationStarted;
    public bool IsNotificationStarted
    {
        get => _isNotificationStarted;
        set => SetProperty(ref _isNotificationStarted, value);
    }
    
    private string _notificationValue;
    public string NotificationValue
    {
        get => _notificationValue;
        set => SetProperty(ref _notificationValue, value);
    }
    
    [Parameter]
    public IBluetoothRemoteGATTCharacteristic Characteristic { get; set; }

    [Parameter]
    public ObservableCollection<string> Logs { get; set; }

    public async Task OnGetDescriptorByUUIDClicked()
    {
        if (string.IsNullOrEmpty(DescriptorUUID))
        {
            return;
        }
        
        Descriptors.Clear();

        try
        {
            var descriptor = await Characteristic.GetDescriptor(DescriptorUUID);
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

    public async Task OnGetDescriptorsByUUIDClicked()
    {
        if (string.IsNullOrEmpty(DescriptorUUID))
        {
            return;
        }

        Descriptors.Clear();

        try
        {
            var descriptors = await Characteristic.GetDescriptors(DescriptorUUID);
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

    public async Task StartNotification()
    {
        try
        {
            await Characteristic.StartNotifications();
            Characteristic.OnRaiseCharacteristicValueChanged += CharacteristicOnOnRaiseCharacteristicValueChanged;
            IsNotificationStarted = true;
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }

    public async Task StopNotification()
    {
        try
        {
            await Characteristic.StopNotifications();
            Characteristic.OnRaiseCharacteristicValueChanged -= CharacteristicOnOnRaiseCharacteristicValueChanged;
            IsNotificationStarted = false;
        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }

    private void CharacteristicOnOnRaiseCharacteristicValueChanged(object? sender, CharacteristicEventArgs e)
    {
        var value = string.Join(" ", e.Value);
        NotificationValue = value;
        Console.WriteLine(value);
    }

    private async Task ReadValue()
    {
        try
        {
            var value = await Characteristic.ReadValue();
            ValueRead = string.Join(" ", value);

        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }
}
