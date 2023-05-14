using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;
using MudBlazor;

namespace WpfMudBlazor;

public partial class MudButtonWrap
{
    private EventAggregatorService? eventAggregatorService;

    private string mudImage = string.Empty;
    private string style = "width:148px; height:48px";

    [Parameter] public string ButtonText { get; set; } = "-";

    [Parameter] public string ButtonId { get; set; } = "-";


    private string buttonImage = "-";
    [Parameter]
    public string ButtonImage
    {
        get
        {
            return buttonImage;
        }
        set
        {
            buttonImage = value;
            switch (buttonImage)
            {
                case "Edit":
                    mudImage = Icons.Material.Filled.Edit;
                    break;
                case "Add":
                    break;
                case "Delete":
                    break;
                case "Save":
                    break;
                default:
                    break;
            }
            style = "width:48px; height:48px";
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            eventAggregatorService = App.AppHost.Services.GetRequiredService<EventAggregatorService>();
            eventAggregatorService.OnButtonPressed += EventAggregatorService_OnButtonPressed;
        }
    }

    private void EventAggregatorService_OnButtonPressed(object? sender, string e)
    {
        StateHasChanged();
    }

    private void ButtonClicked()
    {
        switch (ButtonId)
        {
            case "cancel-button":
                eventAggregatorService?.Publish(new ButtonCancel($"User logged in from Blazor"));
                break;
            case "confirm-button":
                eventAggregatorService?.Publish(new ButtonConfirm($"User logged out from Blazor"));
                break;
        }

    }
}
