using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;

namespace WpfMudBlazor;

public partial class MudButtonWrap
{
    private IEventAggregator? eventAggregator;
    private EventAggregatorService? eventAggregatorService;

    [Parameter] public string ButtonText { get; set; } = "-";


    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();
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
        switch (ButtonText)
        {
            case "Login":
                eventAggregator?.Publish(new ButtonLogin($"User logged in from Blazor"));
                break;
            case "Logout":
                eventAggregator?.Publish(new ButtonLogin($"User logged out from Blazor"));
                break;
        }
        
    }
}
