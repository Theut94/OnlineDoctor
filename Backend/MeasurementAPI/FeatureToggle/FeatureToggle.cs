using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;

namespace MeasurementAPI;

public class FeatureToggle: IFeatureToggle
{
    EdgeFeatureHubConfig _config = null;
    private string _janKey = "7aa23a64-7cc0-43a1-820e-a0dd48c2ce5f/JLT9WVT6k5z4cuQlIH8LdILLf3uBcacZZUfqn5D5";
    private string _mikkelKEy = "d56a33fb-93cd-4afa-ac35-c592b4541be2/yOcmL3wxTTWGD143avq0KFTsjDhBUVK30mrfvD7o";
    private string _simonKEy = "default";
    
    
    public FeatureToggle()
    {
        _config = new EdgeFeatureHubConfig("http://featurehub:8085", _mikkelKEy);
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