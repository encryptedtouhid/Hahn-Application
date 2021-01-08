using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Data.Model;
using Hahn.ApplicatonProcess.December2020.Data.Repository.Interface;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Implementation
{
    public class ApplicantService : BaseService<Applicant>, IApplicantService
    {
        private readonly IApplicantRepository applicantRepository;

        public ApplicantService(IApplicantRepository applicantRepository, IUnitofWork unitofWork, ILogger<Applicant> logger) : base(applicantRepository, unitofWork, logger)
        {
            this.applicantRepository = applicantRepository;
        }
    }
}
