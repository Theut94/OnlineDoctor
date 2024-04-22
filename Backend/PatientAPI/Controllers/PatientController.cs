using Microsoft.AspNetCore.Mvc;
using PatientAPI.Service;
using ShareModels;
using System.Runtime.Intrinsics.X86;
using Monitoring;

namespace PatientAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private IPatientService _patientService;
        private IMonitoringService _monitoringService;

        public PatientController(IPatientService patientServicecs, IMonitoringService monitoringService)
        {
            _patientService = patientServicecs;
            _monitoringService = monitoringService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetPatient(string ssn)
        //{
        //    try
        //    {
        //        var patient = await _patientService.GetPatient(ssn);
        //        return Ok(patient);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            try
            {
                var patient = await _patientService.GetPatients();
                return Ok(patient);
            }
            catch (Exception ex)
            {
                _monitoringService.getLogger().Warning("PATIENT CONTROLLER | Patients could not be fetched - with message {message}", ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult PostPatient(Patient patient)
        {
            try
            {
                _patientService.PostPatient(patient);
                return Ok();
            }
            catch (Exception ex)
            {
                _monitoringService.getLogger().Warning("PATIENT CONTROLLER | Patient {patient} could not be posted - with message {message}", patient, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeletePatient(string ssn)
        {
            try
            {

                _patientService?.DeletePatient(ssn);
                return Ok();
            }
            catch(Exception ex) 
            {
                _monitoringService.getLogger().Warning("PATIENT CONTROLLER | Patient with ssn {ssn} could not be deleted - with message {message}", ssn, ex.Message);
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
