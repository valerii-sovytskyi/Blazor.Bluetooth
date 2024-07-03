using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class AddServicesComponent : BindableBase
{

    private List<string> _services;
    public List<string> Services
    {
        get => _services;
        set
        {
            if (_services != value)
            {
                _services = value;
                SetRef(_services);
            }
        }
    }

    private string _serviceUUID;
    public string ServiceUUID
    {
        get => _serviceUUID;
        set => SetProperty(ref _serviceUUID, value);
    }
    
    [Parameter]
    public Action<List<string>> SetRef { get; set; }

    private void AddService()
    {
        if (Services is null)
        {
            Services = new List<string>();
        }

        Services.Add(ServiceUUID);
        ServiceUUID = string.Empty;
        StateHasChanged();
    }
    
    private void RemoveService(int index)
    {
        Services.RemoveAt(index);
        if (Services.Count == 0)
        {
            Services = null;
        }

        StateHasChanged();
    }

    private void OnServiceTextChanged(int serviceIndex, object arg)
    {
        Services[serviceIndex] = arg.ToString();
    }

}
