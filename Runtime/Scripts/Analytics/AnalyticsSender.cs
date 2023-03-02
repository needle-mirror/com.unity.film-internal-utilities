using System;
using System.Collections.Generic;
using UnityEngine.Analytics;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Unity.FilmInternalUtilities {
internal static class AnalyticsSender {

#if UNITY_EDITOR
    internal static void SendEventInEditor<T>(AnalyticsEvent<T> analyticsEvent) {
        if (!EditorAnalytics.enabled) {
            return;
        }

        if (!IsEventRegistered(analyticsEvent)) {
            if (!RegisterEvent(analyticsEvent)) {
                return;
            }
        }

        if (!ShouldSendEvent(analyticsEvent)) {
            return;
        }

        AnalyticsResult result = EditorAnalytics.SendEventWithLimit(analyticsEvent.eventName, analyticsEvent.parameters, analyticsEvent.version);
        if (result != AnalyticsResult.Ok) {
            return;
        }

        DateTime now = DateTime.Now;
        m_lastSentDateTime[analyticsEvent.eventName] = now;
    }
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    private static bool IsEventRegistered<T>(AnalyticsEvent<T> analyticsEvent) {
        return m_registeredEvents.Contains(analyticsEvent.eventName);
    }

    private static bool ShouldSendEvent<T>(AnalyticsEvent<T> analyticsEvent) {
        if (!m_lastSentDateTime.ContainsKey(analyticsEvent.eventName)) {
            return true;
        }

        DateTime lastSentDateTime = m_lastSentDateTime[analyticsEvent.eventName];
        return DateTime.Now - lastSentDateTime >= analyticsEvent.minInterval;
    }

    private static bool RegisterEvent<T>(AnalyticsEvent<T> analyticsEvent) {
        if (!EditorAnalytics.enabled) {
            return false;
        }

        AnalyticsResult result = EditorAnalytics.RegisterEventWithLimit(analyticsEvent.eventName,
            analyticsEvent.maxEventPerHour, analyticsEvent.maxItems, VENDOR_KEY, analyticsEvent.version);

        if (result != AnalyticsResult.Ok) {
            return false;
        }

        m_registeredEvents.Add(analyticsEvent.eventName);
        return true;
    }


//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    
    private const string VENDOR_KEY = "unity.anime-toolbox";

    private static readonly HashSet<string>              m_registeredEvents = new HashSet<string>();
    private static readonly Dictionary<string, DateTime> m_lastSentDateTime = new Dictionary<string, DateTime>();

#else

    internal static void SendEventInEditor<T>(AnalyticsEvent<T> analyticsEvent) { }

#endif //UNITY_EDITOR
    
}
} //end namespace