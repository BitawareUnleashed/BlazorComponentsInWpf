using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMudBlazor.Models;

namespace WpfMudBlazor;

public partial class MudTextArea
{
    [Parameter] public string Text { get; set; } = "";
    public ISerialCommunication SerialCommunication { get; set; }

    protected override void OnInitialized()
    {
        SerialCommunication = App.AppHost.Services.GetRequiredService<ISerialCommunication>();
        SerialCommunication.SerialChanged += SerialCommunication_SerialChanged;
        SerialCommunication.SerialConnected += SerialCommunication_SerialChanged;
        SerialCommunication.SerialDisconnected += SerialCommunication_SerialChanged;

        base.OnInitialized();
    }

    private void SerialCommunication_SerialChanged(object? sender, string e)
    {
        if(Text.Length > 0)
        {
            Text += $"\n{e}";
        }
        else
        {
            Text = e;
        }
        
        StateHasChanged();
    }
}
