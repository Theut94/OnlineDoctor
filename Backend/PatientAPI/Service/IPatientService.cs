using ShareModels;

namespace PatientAPI.Service
{
    public interface IPatientService
    {
        public void PostPatient(Patient patient);
        public Task<GetPatientDTO> GetPatient(string ssn);

        public void DeletePatient(string ssn);
        public Task<List<GetPatientDTO>> GetPatients();
    }
}
