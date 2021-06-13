using Amatis.PatientObservation.Common.Enums;
using System;

namespace Amatis.PatientObservation.Common.Models.PatientInfoModels
{
    public class PatientInfoModel
    {
        public long Id { get; set; }
        public string PoliclinicCode { get; set; }
        public string DoctorRegistrationNumber { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string DoctorFulName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FulName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string IdentificationNumber { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime LastVisitDate { get; set; }
        public string DoctorNote { get; set; }
    }
}
