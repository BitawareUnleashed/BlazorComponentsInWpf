using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMudBlazor.Models;

namespace WpfMudBlazor.Services;

public class EventAggregatorService : ISubscriber<ButtonLogin>, ISubscriber<ButtonLogout>, ISubscriber<TextChanged>, ISubscriber<PasswordChanged>
{
    public event EventHandler<string> OnButtonPressed;
    public event EventHandler<string> OnTextChanged;
    public event EventHandler<string> OnPasswordChanged;
    void ISubscriber<ButtonLogin>.OnEventRaised(ButtonLogin e)
    {
        OnButtonPressed?.Invoke(this, e.Text);
    }

    void ISubscriber<ButtonLogout>.OnEventRaised(ButtonLogout e)
    {
        OnButtonPressed?.Invoke(this, e.Text);
    }

    void ISubscriber<TextChanged>.OnEventRaised(TextChanged e)
    {
        OnTextChanged?.Invoke(this, e.Text);
    }
    void ISubscriber<PasswordChanged>.OnEventRaised(PasswordChanged e)
    {
        OnPasswordChanged?.Invoke(this, e.Text);
    }


    public EventAggregatorService(IEventAggregator ea)
    {
        ea.SubsribeEvent(this);
    }
}

