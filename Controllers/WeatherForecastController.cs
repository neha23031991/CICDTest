using Microsoft.AspNetCore.Mvc;

namespace SampleNetProjectAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetUserData/{username}")]
        public IActionResult GetUserData(string username)
        {
            // This is vulnerable to SQL injection because it uses string concatenation.
            var sqlQuery = "SELECT * FROM Users WHERE Username = '" + username + "'";
        
            // In a real application, this query would be executed against a database.
            // An attacker could pass ' OR 1=1 --' as the username to bypass authentication
            // and access all user data.
        
            return Ok(sqlQuery); 
        }
    }
}
