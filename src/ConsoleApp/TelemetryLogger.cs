using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace ConsoleApp
{
    public static class TelemetryLogger
    {
        private static readonly TelemetryClient TelemetryClient;

        static TelemetryLogger()
        {
            TelemetryClient = new TelemetryClient(TelemetryConfiguration.Active);
        }

        public static void LogTrace(string message, [CallerFilePath]string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber]int lineNumber = 0)
        {
            TelemetryClient.TrackTrace(message, CreateProperties(filePath, memberName, lineNumber));
        }

        public static void LogEvent(string eventName, [CallerFilePath]string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber]int lineNumber = 0)
        {
            TelemetryClient.TrackEvent(eventName, CreateProperties(filePath, memberName, lineNumber));
        }

        public static void LogException(Exception exception, [CallerFilePath]string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber]int lineNumber = 0)
        {
            TelemetryClient.TrackException(exception, CreateProperties(filePath, memberName, lineNumber));
        }

        public static void Flush()
        {
            TelemetryClient.Flush();
        }

        private static IDictionary<string, string> CreateProperties(string filePath, string memberName, int lineNumber)
        {
            return new Dictionary<string, string>
            {
                {"File Path", filePath},
                {"Member Name", memberName},
                {"Line Number", lineNumber.ToString()}
            };
        }
    }
}
