using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMudBlazor.Models;

namespace WpfMudBlazor.Services;

public class EventAggregatorService : IEventAggregatorService, 
    ISubscriber<ButtonClicked>, ISubscriber<TextChanged>
{

    private readonly IEventAggregator eventAggregator;

    public event EventHandler<string> OnButtonPressed;
    public event EventHandler<string> OnTextChanged;

    void ISubscriber<ButtonClicked>.OnEventRaised(ButtonClicked e)
    {
        OnButtonPressed?.Invoke(this, e.Id);
    }

    void ISubscriber<TextChanged>.OnEventRaised(TextChanged e)
    {
        OnTextChanged?.Invoke(this, e.Text);
    }



    public EventAggregatorService(IEventAggregator ea)
    {
        eventAggregator = ea;
        eventAggregator.Subscribe(this);
    }
}

