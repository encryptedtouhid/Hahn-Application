using Hahn.ApplicatonProcess.December2020.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Hahn.ApplicatonProcess.December2020.Domain.Utility.HttpClient;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface;

namespace Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Implementation
{
    public class CountryService : ICountryService
    {
      
        public ILogger<CountryService> _logger { get; }
        public readonly string countryapi = "https://restcountries.eu/rest/v2/name/{{name}}?fullText=true";
        public CountryService(ILogger<CountryService> logger)
        {           
            _logger = logger;
        }
        public async Task<bool> ValidateCountry(string countryname)
        {

            try
            {
                string url = this.countryapi.Replace("{{name}}", countryname);
                var response = await HttpRequestFactory.Get(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}
