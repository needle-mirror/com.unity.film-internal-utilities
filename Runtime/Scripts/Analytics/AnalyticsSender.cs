using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Analytics;

#if UNITY_EDITOR
using UnityEditor;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
#endif


namespace Unity.FilmInternalUtilities {
internal static class AnalyticsSender {

#if UNITY_EDITOR && ENABLE_CLOUD_SERVICES_ANALYTICS
    private struct EventDetail {
        public string assemblyInfo;
        public string packageName;
        public string packageVersion;
    }
    
    internal static void SendEventInEditor(AnalyticsEvent analyticsEvent) {
        if (!EditorAnalytics.enabled) {
            return;
        }

        if (!IsEventRegistered(analyticsEvent)) {
            Assembly assembly = Assembly.GetCallingAssembly();
            if (!RegisterEvent(analyticsEvent, assembly)) {
                return;
            }
        }

        if (!ShouldSendEvent(analyticsEvent)) {
            return;
        }

        analyticsEvent.parameters.actualPackageVersion = m_registeredEvents[analyticsEvent.eventName].packageVersion;
        AnalyticsResult result = EditorAnalytics.SendEventWithLimit(analyticsEvent.eventName, analyticsEvent.parameters, analyticsEvent.version);
        if (result != AnalyticsResult.Ok) {
            return;
        }

        DateTime now = DateTime.Now;
        m_lastSentDateTime[analyticsEvent.eventName] = now;
    }

    
    
    internal static void FindCallingAssemblyAndPackageInfo([CanBeNull] out Assembly assembly, [CanBeNull] out PackageInfo packageInfo) {
        assembly    = Assembly.GetCallingAssembly();
        if (null == assembly) {
            packageInfo = null;
            return;
        }
        packageInfo = UnityEditor.PackageManager.PackageInfo.FindForAssembly(assembly);
    }
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    private static bool IsEventRegistered(AnalyticsEvent analyticsEvent) {
        return m_registeredEvents.ContainsKey(analyticsEvent.eventName);
    }

    private static bool ShouldSendEvent(AnalyticsEvent analyticsEvent) {
        if (!m_lastSentDateTime.ContainsKey(analyticsEvent.eventName)) {
            return true;
        }

        DateTime lastSentDateTime = m_lastSentDateTime[analyticsEvent.eventName];
        return DateTime.Now - lastSentDateTime >= analyticsEvent.minInterval;
    }

    private static bool RegisterEvent(AnalyticsEvent analyticsEvent, Assembly assembly) {
        if (!EditorAnalytics.enabled) {
            return false;
        }

        AnalyticsResult result = EditorAnalytics.RegisterEventWithLimit(analyticsEvent.eventName,
            analyticsEvent.maxEventPerHour, analyticsEvent.maxItems, VENDOR_KEY, analyticsEvent.version);

        if (result != AnalyticsResult.Ok) {
            return false;
        }

        EventDetail eventDetails = new EventDetail {
            assemblyInfo = assembly.FullName,
        };
        
        PackageInfo packageInfo = UnityEditor.PackageManager.PackageInfo.FindForAssembly(assembly);
        if (packageInfo != null) {
            eventDetails.packageName = packageInfo.name;
            eventDetails.packageVersion = packageInfo.version;
        }

        m_registeredEvents[analyticsEvent.eventName] = eventDetails;
        return true;
    }

    [Obsolete("Use SendEventInEditor instead")]
    internal static void SendEventInEditor<T>(AnalyticsEvent<T> analyticsEvent) {
        SendEventInEditor((AnalyticsEvent)analyticsEvent);
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    
    private const string VENDOR_KEY = "unity.anime-toolbox";

    private static readonly Dictionary<string, EventDetail> m_registeredEvents = new Dictionary<string, EventDetail>();
    private static readonly Dictionary<string, DateTime>    m_lastSentDateTime = new Dictionary<string, DateTime>();

#else

    internal static void SendEventInEditor(AnalyticsEvent analyticsEvent) { }

    [Obsolete("Use SendEventInEditor instead")]
    internal static void SendEventInEditor<T>(AnalyticsEvent<T> analyticsEvent) { }

#endif //UNITY_EDITOR
    
}
} //end namespace