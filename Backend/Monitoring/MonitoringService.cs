using System.Diagnostics;
using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace Monitoring;

public class MonitoringService : IMonitoringService
{
    public ILogger _logger => Serilog.Log.Logger;
    
    public readonly string ServiceName = Assembly.GetCallingAssembly().GetName().Name ?? "Unknown";
    public TracerProvider _tracerProvider;
    public ActivitySource _activitySource;
    
    public MonitoringService()
    {
        _activitySource = new ActivitySource(ServiceName);
        
        _tracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddConsoleExporter()
            .AddSource(_activitySource.Name)
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName))
            .Build();
        
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Seq( "http://seq:5341")
            .CreateLogger();   
    }

    public ILogger getLogger()
    {
        return _logger;
    }

    public ActivitySource getActivitySource()
    {
        return _activitySource;
    }
}