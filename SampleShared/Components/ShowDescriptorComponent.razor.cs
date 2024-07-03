using Microsoft.AspNetCore.Components;
using Blazor.Bluetooth;
using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;

namespace SampleShared.Components;
public partial class ShowDescriptorComponent : BindableBase
{
    [Parameter]
    public IBluetoothRemoteGATTDescriptor Descriptor { get; set; }
    
    [Parameter]
    public ObservableCollection<string> Logs { get; set; }
    
    private byte[] _readWrite;
    public byte[] ValueWrite
    {
        get => _readWrite;
        set => SetProperty(ref _readWrite, value);
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
    
    private string _readRead;
    public string ValueRead
    {
        get => _readRead;
        set => SetProperty(ref _readRead, value);
    }

    private async Task ReadValue()
    {
        try
        {
            var value = await Descriptor.ReadValue();
            ValueRead = string.Join(" ", value);

        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }

    private async Task WriteValue()
    {
        try
        {
            await Descriptor.WriteValue(ValueWrite);

        }
        catch (Exception e)
        {
            Logs.Add(e.Message);
        }
    }
}
