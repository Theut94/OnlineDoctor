using ShareModels;
using SQLitePCL;

namespace MeasurementAPI.Repositories;

public class MeasurementRepository: IMeasurementRepository
{
    public Context _context;
    
    public MeasurementRepository(Context context)
    {
        _context = context;
    }
    
    public Measurement InsertMeasurement(Measurement measurement)
    {
        _context.Add(measurement);
        _context.SaveChanges();
        return measurement;
    }

    public List<Measurement> GetMeassurements(string ssn)
    {
        return _context.Measurements.Where(m => m.patientSSN == ssn).ToList();
    }
}