using Amatis.PatientObservation.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amatis.PatientObservation.Common.Entities
{
    public class PatientInfoEntity : BaseEntity
    {
        [Required(ErrorMessage = "Please enter PoliclinicCode")]
        [Column(TypeName = "varchar(4)")]
        public string PoliclinicCode { get; set; }

        [Required(ErrorMessage = "Please enter DoctorRegistrationNumber")]
        [Column(TypeName = "varchar(8)")]
        public string DoctorRegistrationNumber { get; set; }

        [Required(ErrorMessage = "Please enter DoctorName")]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "Please enter DoctorSurname")]
        public string DoctorSurname { get; set; }

        [NotMapped]
        public string DoctorFulName
        {
            get { return $"{this.DoctorName} {this.DoctorSurname}"; }
        }

        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Surname")]
        public string Surname { get; set; }

        [NotMapped]
        public string FulName
        {
            get { return $"{this.Name} {this.Surname}"; }
        }

        [Required(ErrorMessage = "Please enter BirthDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Please enter Gender")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Please enter IdentificationNumber")]
        [Column(TypeName = "varchar(11)")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Please enter PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter VisitDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime VisitDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastVisitDate { get; set; }

        [Column(TypeName = "varchar(1000)")]
        public string DoctorNote { get; set; }
    }
}
