using Newtonsoft.Json;
using PatientAPI.Repositories;
using ShareModels;

namespace PatientAPI.Service
{
    public class PatientService : IPatientService
    {

        private IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
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

        public void PostPatient(Patient patient)
        {
            _repository.PostPatient(patient);
        }
    }
}
