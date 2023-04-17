using System;

namespace WpfMudBlazor.Models;

public interface IEventAggregator
{
    void PublishEvent<TEventType>(TEventType eventToPublish);

    void SubsribeEvent(Object subscriber);
}
