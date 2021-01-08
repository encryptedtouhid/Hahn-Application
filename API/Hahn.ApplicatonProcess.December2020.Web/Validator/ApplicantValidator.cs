using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Data.Model;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Validator
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator(ICountryService countrySevice)
        {
            RuleFor(x => x.name).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.familyName).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(x => x.address).NotEmpty().NotNull().MinimumLength(10);
            RuleFor(x => x.countryOfOrigin).NotEmpty().NotNull().Must(x => countrySevice.ValidateCountry(x).Result == true).WithMessage("invalid country input");
            RuleFor(x => x.emailAddress).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.age).NotNull().NotEmpty().GreaterThanOrEqualTo(20).LessThanOrEqualTo(60);
        }
    }
}
