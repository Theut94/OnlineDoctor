using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;
using Monitoring;

namespace MeasurementAPI;

public class FeatureToggle: IFeatureToggle
{
    EdgeFeatureHubConfig _config = null;
    private string _janKey = "7aa23a64-7cc0-43a1-820e-a0dd48c2ce5f/JLT9WVT6k5z4cuQlIH8LdILLf3uBcacZZUfqn5D5";
    private string _simonKEy = "005c0035-ef49-4bb4-82c2-63e81dead015/oz0QAq7IZIVtkROqLR0csh2lrKVlsxr0ivQnaKqF";
    private string _simonKEyDesktop = "5adfae95-5569-4464-aac0-4e4f1caa954e/ktxFMqQvRZDBKJ7PEfkjkdqAP0A1uMPGFxS4rETI";
    private string _mikkelKEy = "d56a33fb-93cd-4afa-ac35-c592b4541be2/yOcmL3wxTTWGD143avq0KFTsjDhBUVK30mrfvD7o";
    
    private readonly IMonitoringService _monitoringService;
    
    public FeatureToggle(IMonitoringService monitoringService)
    {
        _config = new EdgeFeatureHubConfig("http://featurehub:8085", _janKey);
        var logger = monitoringService.getLogger();
        FeatureLogging.TraceLogger += (sender, s) => logger.Information(s);
        FeatureLogging.InfoLogger += (sender, s) => logger.Information(s);
        FeatureLogging.ErrorLogger += (sender, s) => logger.Error(s);
       
    }
    
    public async Task<bool> IsCountryAllowed(string country)
    {
        StrategyAttributeCountryName SACM;
        var couldParse = Enum.TryParse(country, true, out SACM);
        if (couldParse)
        {
            var fh = await _config.NewContext().Country(SACM).Build();
            if(fh["DanishAccess"].IsEnabled)
            {
                return true;
            }
        }
        return false;
    }
}