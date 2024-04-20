using FeatureHubSDK;
using MeasurementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ShareModels;

namespace MeasurementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _service;
        private readonly IFeatureToggle _featureToggle;
        

        public MeasurementController(IMeasurementService service, IFeatureToggle featureToggle)
        {
            _service = service;
            _featureToggle = featureToggle;
        }

        [HttpPost(Name = "PostMeasurement")]
        public async Task<IActionResult> Post(MeasurementPostDTO measurementPostDTO)
        {
            string country = HttpContext.Request.Query["country"];

            var feature = await _featureToggle.IsCountryAllowed(country);
            if (!feature)
            {
                return StatusCode(403, "Country not allowed");
            }
            
            var result = _service.InsertMeasurement(measurementPostDTO);
            if (result == null || result.Id == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }        
        
        [HttpPut(Name = "PutMeasurement")]
        public async Task<IActionResult> Put(Measurement measurement)
        {
            string country = HttpContext.Request.Query["country"];
            
            var feature = await _featureToggle.IsCountryAllowed(country);
            if (!feature)
            {
                return StatusCode(403, "Country not allowed");
            }
            
            var result = _service.UpdateMeasurement(measurement);
            if (result == null || result.Id == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        
        [HttpGet(Name = "GetMeasurements")]
        public async Task<IActionResult> Get(string ssn)
        {
            var result =  await _service.GetMeassurements(ssn);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
