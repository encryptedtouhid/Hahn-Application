using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface;
using Hahn.ApplicatonProcess.December2020.Domain.ViewModels;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherRepository;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,  IWeatherService weatherService)
        {
            _logger = logger;
            _weatherRepository = weatherService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherRepository.GetWeatherForecasts();
        }
    }
}
