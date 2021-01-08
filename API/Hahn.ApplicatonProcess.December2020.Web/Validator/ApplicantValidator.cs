using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Data.Model;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface;
using Hahn.ApplicatonProcess.December2020.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Validator
{
    public class ApplicantValidator : AbstractValidator<ApplicantVM>
    {
        public ApplicantValidator(ICountryService countrySevice)
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.FamilyName).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.Address).NotEmpty().NotNull().MinimumLength(10);
            RuleFor(x => x.CountryOfOrigin).NotEmpty().NotNull().Must(x => countrySevice.ValidateCountry(x).Result == true).WithMessage("invalid country input");
            RuleFor(x => x.EmailAddress).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Age).NotNull().NotEmpty().GreaterThanOrEqualTo(20).LessThanOrEqualTo(60);
        }
    }
}
