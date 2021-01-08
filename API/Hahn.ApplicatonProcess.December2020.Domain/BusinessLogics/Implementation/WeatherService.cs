using System;
using System.Collections.Generic;
using System.Linq;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface;
using Hahn.ApplicatonProcess.December2020.Domain.ViewModels;

namespace Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Implementation
{
    public class WeatherService : IWeatherService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToList();
        }
    }
}