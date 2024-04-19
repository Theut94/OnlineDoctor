using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Measurement>>GetMeassurements(string ssn)
    {
        return await _context.Measurements.Where(m => m.patientSSN == ssn).ToListAsync();
    }
}