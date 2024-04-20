namespace MeasurementAPI;

public interface IFeatureToggle
{
    public Task<bool> IsCountryAllowed(string country);
}