using Amatis.PatientObservation.Common.Enums;
using Amatis.PatientObservation.Common.Models.Filter;
using System;

namespace Amatis.PatientObservation.Common.Models.PatientInfoModels
{
    public class PatientInfoQueryModel : PaginationFilter
    {
        public DateTime? VisitStartDate { get; set; }

        public DateTime? VisitEndDate { get; set; }

        public string PoliclinicCode { get; set; }

        public string DoctorRegistrationNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string DoctorName { get; set; }

        public string DoctorSurname { get; set; }

        public DateTime? BirthStartDate { get; set; }

        public DateTime? BirthEndDate { get; set; }

        public Gender? Gender { get; set; }

    }
}
