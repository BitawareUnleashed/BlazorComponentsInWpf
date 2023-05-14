using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMudBlazor.Models;

namespace WpfMudBlazor.Services;

public class EventAggregatorService :
    ISubscriber<ButtonConfirm>,
    ISubscriber<ButtonCancel>,
    ISubscriber<TextChanged>,
    ISubscriber<PasswordChanged>,
    ISubscriber<ButtonClick>,
    ISubscriber<NextCustomerRequest>
{

    private readonly IEventAggregator? eventAggregator;

    public event EventHandler<string>? OnButtonPressed;
    public event EventHandler<string>? OnTextChanged;
    public event EventHandler<string>? OnPasswordChanged;
    public event EventHandler<string>? OnButtonClickedChanged;
    public event EventHandler<Customer>? OnCustomerChanged;

    void ISubscriber<ButtonConfirm>.OnEventRaised(ButtonConfirm e) => OnButtonPressed?.Invoke(this, e.Text);

    void ISubscriber<ButtonCancel>.OnEventRaised(ButtonCancel e) => OnButtonPressed?.Invoke(this, e.Text);

    void ISubscriber<TextChanged>.OnEventRaised(TextChanged e) => OnTextChanged?.Invoke(this, e.Text);

    void ISubscriber<PasswordChanged>.OnEventRaised(PasswordChanged e) => OnPasswordChanged?.Invoke(this, e.Text);

    void ISubscriber<ButtonClick>.OnEventRaised(ButtonClick e) => OnButtonClickedChanged?.Invoke(this, e.Text);

    void ISubscriber<NextCustomerRequest>.OnEventRaised(NextCustomerRequest e)
    {
        // Recuperare il customer
        OnCustomerChanged?.Invoke(this, new Customer());
    }

    public EventAggregatorService(IEventAggregator ea)
    {
        eventAggregator = ea;
        eventAggregator.Subscribe(this);
    }

    public void Publish<T>(T p)
    {
        eventAggregator?.Publish(p);
    }

    
}

