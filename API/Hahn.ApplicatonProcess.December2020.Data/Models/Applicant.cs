using Hahn.ApplicatonProcess.December2020.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Data.Model
{
    public class Applicant : BaseEntity
    {
        public string name { get; set; }
        public string familyName { get; set; }
        public string address { get; set; }
        public string countryOfOrigin { get; set; }
        public string emailAddress { get; set; }
        public int age { get; set; }
        public bool hired { get; set; } = false;
    }
}
