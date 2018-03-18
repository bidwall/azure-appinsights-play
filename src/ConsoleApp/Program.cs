using System;
using System.IO;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    class Program
    {
        private static TelemetryClient _telemetryClient;

        static void Main(string[] args)
        {
            SetupTelemetry();
            WriteTelemetry();

            //Console.ReadKey();
        }

        static void SetupTelemetry()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");

            var instrumentationKey = builder.Build()["instrumentationKey"];

            TelemetryConfiguration.Active.InstrumentationKey = instrumentationKey;

            _telemetryClient = new TelemetryClient();
        }

        static void WriteTelemetry()
        {
            WriteTrace("message");
            WriteEvent("event");
            WriteException(new Exception("Exception"));

            _telemetryClient.Flush();       // more efficient not to flush, but ok for playing ;-)
        }

        static void WriteTrace(string message)
        {
            TelemetryLogger.LogTrace(message);
        }

        static void WriteEvent(string eventName)
        {
            TelemetryLogger.LogEvent(eventName);
        }

        static void WriteException(Exception exception)
        {
            TelemetryLogger.LogException(exception);
        }
    }
}
