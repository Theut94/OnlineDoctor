using Monitoring;
using Newtonsoft.Json;
using PatientAPI.Repositories;
using ShareModels;

namespace PatientAPI.Service
{
    public class PatientService : IPatientService
    {

        private IPatientRepository _repository;
        private IMonitoringService _monitoringService;

        public PatientService(IPatientRepository repository, IMonitoringService monitoringService)
        {
            _repository = repository;
            _monitoringService = monitoringService;
        }
        public void DeletePatient(string ssn)
        {
            _repository.DeletePatient(ssn);
        }

        public async Task<GetPatientDTO> GetPatient(string ssn)
        {

            var patient = _repository.GetPatient(ssn);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://measurement/Measurement?ssn={ssn}");
            
            var response = await client.SendAsync(request);
            var Measurements = new List<Measurement>();
            if (response.IsSuccessStatusCode) 
            { 
                var result = await response.Content.ReadAsStringAsync();            
                Measurements = JsonConvert.DeserializeObject<List<Measurement>>(result);
            }
            //Should use a mapper weee

            var patientDTO = new GetPatientDTO()
            {
                mail = patient.mail,
                name = patient.name,
                ssn = ssn,
                measurements= Measurements
            };
            return patientDTO;
        }

        public async Task<List<GetPatientDTO>> GetPatients()
        {
            var patients = _repository.GetPatients();
            var patientDTOs = new List<GetPatientDTO>();

            var client = new HttpClient();

            foreach (var patient in patients) // Here 'patient' is the singular form
            {
                string ssn = patient.ssn; // Assuming that SSN is a property of Patient


                var request = new HttpRequestMessage(HttpMethod.Get, $"http://measurement/Measurement?ssn={ssn}");


                var response = await client.SendAsync(request);
                var measurements = new List<Measurement>();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    measurements = JsonConvert.DeserializeObject<List<Measurement>>(result);
                }
                else
                {
                    _monitoringService.getLogger().Warning("PATIENT SERVICE | Could not fetch measurements for patient {patient}", patient); 
                }
                //Should use a mapper weee

                var patientDTO = new GetPatientDTO()
                {
                    mail = patient.mail,
                    name = patient.name,
                    ssn = ssn,
                    measurements = measurements
                };
                patientDTOs.Add(patientDTO);
            }
            return patientDTOs;
        }

        public void PostPatient(Patient patient)
        {
            _repository.PostPatient(patient);
        }
    }
}
