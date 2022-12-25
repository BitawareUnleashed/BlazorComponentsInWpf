using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;

namespace WpfMudBlazor;

public partial class MudButtonWrap
{
    private IEventAggregator? eventAggregator;
    private EventAggregatorService? eventAggregatorService;
    private string buttonName = "My Button";
    protected override void OnInitialized()
    {
        if (eventAggregator is not null)
        {
            eventAggregator.PublishEvent(new ButtonPressed() { ButtonName = "Blazor" });
        }
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("RegisterWPFApp", DotNetObjectReference.Create(this));

            eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();
            eventAggregatorService = App.AppHost.Services.GetRequiredService<EventAggregatorService>();
            eventAggregatorService.OnButtonPressed += EventAggregatorService_OnButtonPressed;
        }
    }

    private void EventAggregatorService_OnButtonPressed(object? sender, string e)
    {
        buttonName = e;
        StateHasChanged();
    }

    private void ButtonClicked()
    {
        eventAggregator?.PublishEvent(new ButtonPressed() { ButtonName = "Blazor" });
    }
}
