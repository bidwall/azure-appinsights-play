using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ApplicationInsights;

namespace ConsoleApp
{
    public static class TelemetryLogger
    {
        private static readonly TelemetryClient TelemetryClient;

        static TelemetryLogger()
        {
            TelemetryClient = new TelemetryClient();
        }

        public static void LogTrace(string message, [CallerFilePath]string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber]int lineNumber = 0)
        {
            TelemetryClient.TrackTrace(message, CreateProperties(filePath, memberName, lineNumber));
        }

        public static void LogEvent(string eventName)
        {
            TelemetryClient.TrackEvent(eventName);
        }

        public static void LogException(Exception exception)
        {
            TelemetryClient.TrackException(exception);
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
