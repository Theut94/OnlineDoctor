using ShareModels;

namespace MeasurementAPI.Repositories;

public interface IMeasurementRepository
{
    Measurement InsertMeasurement(Measurement measurement);
    List<Measurement> GetMeassurements(String ssn);
}