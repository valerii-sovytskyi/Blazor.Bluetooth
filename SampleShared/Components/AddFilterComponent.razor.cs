using Blazor.Bluetooth;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace SampleShared.Components;
public partial class AddFilterComponent : BindableBase
{
    private List<Filter> _filters;
    public List<Filter> Filters
    {
        get => _filters;
        set
        {
            if (_filters != value)
            {
                _filters = value;
                SetRef(_filters);
            }
        }
    }
    
    [Parameter]
    public Action<List<Filter>> SetRef { get; set; }
    
    private void AddNewFilter()
    {
        if (Filters is null)
        {
            Filters = new List<Filter>();
        }

        Filters.Add(new Filter());
        StateHasChanged();
    }

    private void RemoveFilter(Filter filter)
    {
        Filters.Remove(filter);

        if (Filters.Count == 0)
        {
            Filters = null;
        }
        StateHasChanged();
    }

}
