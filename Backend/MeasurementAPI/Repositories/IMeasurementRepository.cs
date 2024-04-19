using ShareModels;

namespace MeasurementAPI.Repositories;

public interface IMeasurementRepository
{
    Measurement InsertMeasurement(Measurement measurement);
    Task<List<Measurement>> GetMeassurements(String ssn);
}