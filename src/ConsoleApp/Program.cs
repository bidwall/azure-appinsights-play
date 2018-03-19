using System;
using System.IO;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupTelemetry();
            WriteTelemetry();

            Console.ReadKey();
        }

        static void SetupTelemetry()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");

            TelemetryConfiguration.Active.InstrumentationKey = builder.Build()["instrumentationKey"];
        }

        static void WriteTelemetry()
        {
            WriteTrace("some message");
            WriteEvent("some event");
            WriteException(new Exception("some exception"));

            TelemetryLogger.Flush();       // more efficient not to flush, but ok for playing ;-)
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
