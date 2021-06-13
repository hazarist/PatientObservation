using Amatis.PatientObservation.Common.Models.PatientInfoModels;
using FluentValidation;

namespace Amatis.PatientObservation.Common.Validators
{
    public class PatientInfoPutModelValidator : AbstractValidator<PatientInfoPutModel>
    {
        public PatientInfoPutModelValidator()
        {
            RuleFor(patientInfo => patientInfo.PoliclinicCode)
                .NotNull()
                .NotEmpty()
                .Length(4);

            RuleFor(patientInfo => patientInfo.DoctorRegistrationNumber)
                .NotNull()
                .NotEmpty()
                .Must(x => int.TryParse(x, out var val) && val > 0)
                .WithMessage("Invalid registration number")
                .Length(8);

            RuleFor(patientInfo => patientInfo.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(patientInfo => patientInfo.Surname)
                .NotNull()
                .NotEmpty();

            RuleFor(patientInfo => patientInfo.DoctorName)
                .NotNull()
                .NotEmpty();

            RuleFor(patientInfo => patientInfo.DoctorSurname)
                .NotNull()
                .NotEmpty();

            RuleFor(patientInfo => patientInfo.BirthDate)
                .NotNull()
                .NotEmpty();

            RuleFor(patientInfo => patientInfo.Gender)
                .IsInEnum();

            RuleFor(patientInfo => patientInfo.IdentificationNumber)
                .NotNull()
                .NotEmpty()
                .Must(x => long.TryParse(x, out var val) && val > 0)
                .WithMessage("Invalid identification number")
                .Length(11);

            RuleFor(patientInfo => patientInfo.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .Matches("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")
                .WithMessage("Invalid phone number");

            RuleFor(patientInfo => patientInfo.VisitDate)
                .NotNull()
                .NotEmpty();

            RuleFor(patientInfo => patientInfo.DoctorNote)
                .MaximumLength(1000);

        }
    }
}
