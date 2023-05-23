using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;

namespace WpfMudBlazor;

public partial class MudTextFieldWrap
{
    private IEventAggregator? eventAggregator;
    private EventAggregatorService? eventAggregatorService;

    [Parameter] public string LabelText { get; set; } = string.Empty;

    [Parameter] public InputType InputType { get; set; } = InputType.Text;


    private string textValue = string.Empty;

    public string TextValue
    {
        get => textValue;
        set
        {
            textValue = value;
            eventAggregator.Publish(new TextChanged(textValue));
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();
            eventAggregatorService = App.AppHost.Services.GetRequiredService<EventAggregatorService>();
        }
    }
}
