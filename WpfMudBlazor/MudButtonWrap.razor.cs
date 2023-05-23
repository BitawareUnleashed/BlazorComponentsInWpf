using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WpfMudBlazor;

public partial class MudButtonWrap
{
    private string mudImage = string.Empty;
    private string style = "width:148px; height:48px";

    [Parameter] public string ButtonText { get; set; } = "-";

    [Parameter] public string ButtonId { get; set; } = "-";

    
    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    private void EventAggregatorService_OnButtonPressed(object? sender, string e)
    {
        StateHasChanged();
    }

    public void ButtonClicked()
    {

    }
}
