using System.Diagnostics;
using OpenTelemetry.Trace;
using Serilog;

namespace Monitoring;

public interface IMonitoringService
{
    public ILogger getLogger();
    
    public ActivitySource getActivitySource();
}