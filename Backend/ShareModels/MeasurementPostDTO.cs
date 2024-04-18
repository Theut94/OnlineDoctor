namespace ShareModels;

public class MeasurementPostDTO
{
    public DateTime date { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public string patientSSN { get; set; }
}
