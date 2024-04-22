using ShareModels;

namespace PatientAPI.Repositories
{
    public interface IPatientRepository
    {

        public void PostPatient(Patient patient);
        public Patient GetPatient(string ssn);

        public void DeletePatient(string ssn);
        public List<Patient> GetPatients();
    }
}
