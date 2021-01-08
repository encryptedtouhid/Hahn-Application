using System.Collections.Generic;
using Hahn.ApplicatonProcess.December2020.Domain.ViewModels;

namespace Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts();
    }
}