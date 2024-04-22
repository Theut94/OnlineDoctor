using FeatureHubSDK;
using MeasurementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Monitoring;
using ShareModels;

namespace MeasurementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _service;
        private readonly IFeatureToggle _featureToggle;
        private readonly IMonitoringService _monitoringService;
        
        public MeasurementController(IMeasurementService service, IFeatureToggle featureToggle, IMonitoringService monitoringService)
        {
            _service = service;
            _featureToggle = featureToggle;
            _monitoringService = monitoringService;
        }

        [HttpPost(Name = "PostMeasurement")]
        public async Task<IActionResult> Post(MeasurementPostDTO measurementPostDTO)
        {
            using var activity = _monitoringService.getActivitySource().StartActivity("TestActivity");
            string country = HttpContext.Request.Query["country"];

                var feature = await _featureToggle.IsCountryAllowed(country);
                if (!feature)
                {
                    _monitoringService.getLogger()
                        .Information(
                            "MEASUREMENT CONTROLLER | Request was denied by feature toggle. body = {measurementPostDTO}, query param = {country}",
                            measurementPostDTO, country);
                    return StatusCode(403, "Country not allowed");
                }

                var result = _service.InsertMeasurement(measurementPostDTO);
                if (result == null || result.Id == null)
                {
                    _monitoringService.getLogger()
                        .Warning(
                            "MEASUREMENT CONTROLLER | Measurement could not be inserted into the database. {measurementPostDTO}",
                            measurementPostDTO);
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
                _monitoringService.getLogger().Information("MEASUREMENT CONTROLLER | Request was denied by feature toggle. body = {measurement}, query param = {country}", measurement, country);
                return StatusCode(403, "Country not allowed");
            }
            
            var result = _service.UpdateMeasurement(measurement);
            if (result == null || result.Id == null)
            {
                _monitoringService.getLogger().Warning("MEASUREMENT CONTROLLER | Measurement could not be updated in the database. {measurement}", measurement);
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
