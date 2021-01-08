using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Model;
using Hahn.ApplicatonProcess.December2020.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.MappingProfile
{
    public class ApplicantMappingProfile : Profile
    {
        public ApplicantMappingProfile()
        {
            CreateMap<ApplicantVM, Applicant>();
            CreateMap<Applicant, ApplicantVM>();
        }
    }
}
