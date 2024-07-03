using Microsoft.AspNetCore.Components;

namespace SampleShared.Components;
public partial class InputTextToByteArrayComponent
{
    public bool IsTextValid { get; set; } = true;

    private string _stringValue;

    public string StringValue
    {
        get => _stringValue;
        set
        {
            if (_stringValue != value)
            {
                _stringValue = value;
                Value = TryParseStringValue();
                ValueChanged.InvokeAsync(Value);
                StateHasChanged();
            }
        }
    }
    
    [Parameter]
    public byte[] Value { get; set; }

    [Parameter]
    public EventCallback<byte[]> ValueChanged { get; set; }

    private byte[] TryParseStringValue()
    {
        if (string.IsNullOrEmpty(StringValue))
        {
            IsTextValid = true;
            return Array.Empty<byte>();
        }

        try
        {
            var bytes = StringValue.Trim().Split(" ");
            var value = new byte[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                
                if (byte.TryParse(bytes[i], out var valueByte))
                {
                    value[i] = valueByte;
                }
                else
                {
                    IsTextValid = false;
                    return Array.Empty<byte>();
                }
            }
            
            IsTextValid = true;
            return value;
        }
        catch (Exception e)
        {
            IsTextValid = false;
        }
        
        IsTextValid = false;
        return Array.Empty<byte>();
    }

}