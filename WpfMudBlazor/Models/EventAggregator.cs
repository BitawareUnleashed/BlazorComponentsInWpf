using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WpfMudBlazor.Models;

internal class EventAggregator : IEventAggregator
{
    private readonly Dictionary<Type, List<WeakReference>> subscribersListByType = new Dictionary<Type, List<WeakReference>>();

    private readonly object lockSubscriberDictionary = new object();


    #region IEventAggregator

    /// <inheritdoc cref="IEventAggregator"/>
    public void Publish<TEventType>(TEventType eventToPublish)
    {
        var subsriberType = typeof(ISubscriber<>).MakeGenericType(typeof(TEventType));

        var subscribers = GetSubscriberList(subsriberType);

        List<WeakReference> subsribersToBeRemoved = new List<WeakReference>();

        foreach (var weakSubsriber in subscribers)
        {
            if (weakSubsriber.IsAlive)
            {
                var subscriber = (ISubscriber<TEventType>)weakSubsriber.Target;

                InvokeSubscriberEvent<TEventType>(eventToPublish, subscriber);
            }
            else
            {
                subsribersToBeRemoved.Add(weakSubsriber);
            }
        }


        if (subsribersToBeRemoved.Any())
        {
            lock (lockSubscriberDictionary)
            {
                foreach (var remove in subsribersToBeRemoved)
                {
                    subscribers.Remove(remove);
                }
            }
        }
    }

    /// <inheritdoc cref="IEventAggregator"/>
    public void Subscribe(object subscriber)
    {
        lock (lockSubscriberDictionary)
        {
            var subsriberTypes = subscriber.GetType().GetInterfaces()
                                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));

            WeakReference weakReference = new WeakReference(subscriber);

            foreach (var subsriberType in subsriberTypes)
            {
                List<WeakReference> subscribers = GetSubscriberList(subsriberType);

                subscribers.Add(weakReference);
            }
        }
    }

    #endregion

    /// <summary>
    /// Invokes the subscriber event.
    /// </summary>
    /// <typeparam name="TEventType">The type of the event type.</typeparam>
    /// <param name="eventToPublish">The event to publish.</param>
    /// <param name="subscriber">The subscriber.</param>
    private void InvokeSubscriberEvent<TEventType>(TEventType eventToPublish, ISubscriber<TEventType> subscriber)
    {
        //Synchronize the invocation of method 
        SynchronizationContext syncContext;
        if (SynchronizationContext.Current is null)
        {
            syncContext = new SynchronizationContext();
        }
        else
        {
            syncContext = SynchronizationContext.Current;
        }

        syncContext.Post(s => subscriber.OnEventRaised(eventToPublish), null);
    }

    private List<WeakReference> GetSubscriberList(Type subsriberType)
    {
        List<WeakReference>? subsribersList = null;

        lock (lockSubscriberDictionary)
        {
            bool found = subscribersListByType.TryGetValue(subsriberType, out subsribersList);

            if (!found)
            {
                //First time create the list.
                subsribersList = new List<WeakReference>();
                subscribersListByType.Add(subsriberType, subsribersList);
            }
        }
        return subsribersList;
    }
}
