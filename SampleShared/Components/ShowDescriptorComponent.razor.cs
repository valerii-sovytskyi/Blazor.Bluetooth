using Microsoft.AspNetCore.Components;
using Blazor.Bluetooth;
using System.Collections.ObjectModel;

namespace SampleShared.Components;
public partial class ShowDescriptorComponent : BindableBase
{
    [Parameter]
    public IBluetoothRemoteGATTDescriptor Descriptor { get; set; }
    
    [Parameter]
    public ObservableCollection<string> Logs { get; set; }
    
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
}
