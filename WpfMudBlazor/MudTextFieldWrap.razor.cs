using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WpfMudBlazor;

public partial class MudTextFieldWrap
{
    [Parameter] public string LabelText { get; set; } = string.Empty;

    [Parameter] public InputType InputType { get; set; } = InputType.Text;


    private string textValue = string.Empty;

    public string TextValue
    {
        get => textValue;
        set
        {
            textValue = value;
        }
    }


    private void EventAggregatorService_OnButtonPressed(object? sender, string e)
    {
        StateHasChanged();
    }
}
