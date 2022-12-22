﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMudBlazor.Models;

internal interface IEventAggregator
{
    void PublishEvent<TEventType>(TEventType eventToPublish);

    void SubsribeEvent(Object subscriber);
}
