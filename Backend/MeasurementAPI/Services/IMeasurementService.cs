using MeasurementAPI.Controllers;
using ShareModels;

namespace MeasurementAPI.Services;

public interface IMeasurementService
{
    Measurement InsertMeasurement(MeasurementPostDTO measurement);
    Measurement UpdateMeasurement(Measurement measurement);
    Task<List<Measurement>> GetMeassurements(String ssn);
}