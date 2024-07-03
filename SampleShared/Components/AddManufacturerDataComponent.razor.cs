using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class AddManufacturerDataComponent : BindableBase
{
    private List<ManufacturerData> _manufacturerData;
    public List<ManufacturerData> ManufacturerData
    {
        get => _manufacturerData;
        set
        {
            if (_manufacturerData != value)
            {
                _manufacturerData = value;
                SetRef(_manufacturerData);
            }
        }
    }
    
    [Parameter]
    public Action<List<ManufacturerData>> SetRef { get; set; }

    private void AddData()
    {
        if (ManufacturerData is null)
        {
            ManufacturerData = new List<ManufacturerData>();
        }

        ManufacturerData.Add(new ManufacturerData());
        StateHasChanged();
    }
    
    private void RemoveData(ManufacturerData manufacturerData)
    {
        ManufacturerData.Remove(manufacturerData);
        if (ManufacturerData.Count == 0)
        {
            ManufacturerData = null;
        }

        StateHasChanged();
    }

}
