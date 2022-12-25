using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMudBlazor.Models;

namespace WpfMudBlazor.Services;

public class EventAggregatorService : ISubscriber<ButtonPressed>
{
    public event EventHandler<string> OnButtonPressed;
    void ISubscriber<ButtonPressed>.OnEventRaised(ButtonPressed e)
    {
        OnButtonPressed?.Invoke(this, e.ButtonName);
    }

    public EventAggregatorService(IEventAggregator ea)
    {
        ea.SubsribeEvent(this);
    }
}

