using ShareModels;

namespace PatientAPI.Repositories
{
    public class PatientRepository : IPatientRepository
    {

        private Context _context;
        public PatientRepository(Context context)
        {
            _context = context;
        }

        public void DeletePatient(string ssn)
        {
            _context.Patients.Remove(GetPatient(ssn));
            _context.SaveChanges();
        }

        public Patient GetPatient(string ssn)
        {
           var patient = _context.Patients.FirstOrDefault(x => x.ssn == ssn);
            return patient;
        }

        public void PostPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }
    }
}
