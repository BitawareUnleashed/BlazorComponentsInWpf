namespace WpfMudBlazor.Models;

internal interface ISubscriber<TEventType> 
{
    void OnEventRaised(TEventType e);
}
