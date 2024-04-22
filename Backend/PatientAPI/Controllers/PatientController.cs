using Microsoft.AspNetCore.Mvc;
using PatientAPI.Service;
using ShareModels;
using System.Runtime.Intrinsics.X86;

namespace PatientAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private IPatientService _patientService;

        public PatientController(IPatientService patientServicecs) 
        { 
            _patientService = patientServicecs;
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
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
