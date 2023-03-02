using System;

namespace Unity.FilmInternalUtilities {
internal abstract class AnalyticsEvent<T> {
    
    internal abstract string eventName       { get; }
    internal virtual  int    version         => 1;
    internal virtual  int    maxEventPerHour => 10000;
    internal virtual  int    maxItems        => 1000;

    // Minimum interval to send this event
    internal virtual TimeSpan minInterval => TimeSpan.Zero;

    internal readonly T parameters;

    internal AnalyticsEvent() {
    }

    internal AnalyticsEvent(T eventData) {
        parameters = eventData;
    }
}

} //end namespace