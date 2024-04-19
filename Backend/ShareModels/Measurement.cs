namespace ShareModels
{
    public class Measurement
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public string patientSSN { get; set; }
        public bool HasBeenSeen {  get; set; } = false;
    }
}
