using System;

namespace WpfMudBlazor.Models;

/// <summary>
/// Interfaccia di implementazione per EventAggregator
/// </summary>
public interface IEventAggregator
{
    /// <summary>
    /// Publishes the specified event.
    /// </summary>
    /// <typeparam id="TEventType">The type of the event type.</typeparam>
    /// <param id="eventToPublish">The event to publish.</param>
    void Publish<TEventType>(TEventType eventToPublish);

    /// <summary>
    /// Register a subscribe to specified events.
    /// </summary>
    /// <param id="subscriber">The subscriber.</param>
    void Subscribe(Object subscriber);
}
