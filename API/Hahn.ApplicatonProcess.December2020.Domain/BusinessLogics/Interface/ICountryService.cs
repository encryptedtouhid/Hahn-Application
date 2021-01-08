using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface
{
    public interface ICountryService
    {
        Task<bool> ValidateCountry(string countryname);
    }
}
