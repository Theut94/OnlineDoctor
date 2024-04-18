using MeasurementAPI.Controllers;
using ShareModels;

namespace MeasurementAPI.Services;

public interface IMeasurementService
{
    Measurement InsertMeasurement(MeasurementPostDTO measurement);
    List<Measurement> GetMeassurements(String ssn);
}