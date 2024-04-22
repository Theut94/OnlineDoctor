﻿using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;

namespace MeasurementAPI;

public class FeatureToggle: IFeatureToggle
{
    EdgeFeatureHubConfig _config = null;
    private string _janKey = "7aa23a64-7cc0-43a1-820e-a0dd48c2ce5f/JLT9WVT6k5z4cuQlIH8LdILLf3uBcacZZUfqn5D5";
    private string _mikkelKEy = "default";
    private string _simonKEy = "005c0035-ef49-4bb4-82c2-63e81dead015/oz0QAq7IZIVtkROqLR0csh2lrKVlsxr0ivQnaKqF";
    
    
    public FeatureToggle()
    {
        _config = new EdgeFeatureHubConfig("http://featurehub:8085", _simonKEy);
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