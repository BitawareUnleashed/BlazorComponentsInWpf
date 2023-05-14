using System.Threading.Tasks;
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WpfMudBlazor;

public partial class MudTextFieldWrap
{
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
            PublishMessage(textValue);
        }
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

    private void PublishMessage(string text)
    {
        switch (InputType)
        {
            case InputType.Text:
                eventAggregatorService?.Publish(new TextChanged(TextValue));
                break;
            case InputType.Password:
                eventAggregatorService?.Publish(new PasswordChanged(TextValue));
                break;
            default:
                break;
        }
    }
}
