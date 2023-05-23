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
    [Parameter] [EditorRequired] public string ButtonId { get; set; } = "-";


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

    private void ButtonClicked(string id)
    {
        eventAggregator?.Publish(new ButtonClicked(id));
    }
}
