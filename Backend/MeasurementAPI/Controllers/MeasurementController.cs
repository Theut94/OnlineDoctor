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
        

        public MeasurementController(IMeasurementService service)
        {
            _service = service;
        }

        [HttpPost(Name = "PostMeasurement")]
        public async Task<IActionResult> Post(MeasurementPostDTO measurementPostDTO)
        {
            string country = HttpContext.Request.Query["country"];
            
            Console.WriteLine($"Country: {country}");
            Console.WriteLine(measurementPostDTO);
            
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
            string country = HttpContext.Request.Query["country"]; //
            
            Console.WriteLine($"Country: {country}");
            Console.WriteLine(measurement);
            
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
