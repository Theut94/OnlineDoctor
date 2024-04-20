using ShareModels;

namespace MeasurementAPI.Repositories;

public interface IMeasurementRepository
{
    Measurement InsertMeasurement(Measurement measurement);
    
    Measurement UpdateMeasurement(Measurement measurement);
    Task<List<Measurement>> GetMeassurements(String ssn);
}