using FluentValidation;
using RestApi.Models;

namespace RestApi.Validation
{
    internal class AttendanceValidator : AbstractValidator<AttendanceValid>
    {
        public AttendanceValidator()
        {
            RuleFor(x => x.Mark).Must(x => x == null || (x >= 0 && x <= 5)).WithMessage("Mark must be between 0 and 5 or be empty.")
                .Equal(0).When(x => x.StudentAttended == false).WithMessage("Mark should be 0 if the student did not attend the lecture.");

            RuleFor(x => x.StudentAttended).Must(x => x == false || x == true || x == null).WithMessage("Student attendance must be true, false or empty.")
                .Equal(true).When(x => x.Mark.HasValue && x.Mark > 0 && x.Mark < 5).WithMessage("Student must attend lecture to get mark for homework.");
            RuleFor(x => x.LectureId).NotNull().WithMessage("Lecture id can not be empty.").GreaterThan(0).WithMessage("ID of lecture must be greater than 0.");
            RuleFor(x => x.StudentId).NotNull().WithMessage("Student id can not be empty.").GreaterThan(0).WithMessage("ID of student must be greater than 0.");
        }
    }
}
