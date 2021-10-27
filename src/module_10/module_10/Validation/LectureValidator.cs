using FluentValidation;
using RestApi.Models;

namespace RestApi.Validation
{
    internal class LectureValidator : AbstractValidator<LectureValid>
    {
        public LectureValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Lecture name can not be empty.").Length(1, 30).WithMessage("Lecture name must be more than 1 and less than 30 symbols.");
            RuleFor(x => x.HomeworkId).Must(x => x == null || x > 0).WithMessage("Homework id must be greater than 0.");
            RuleFor(x => x.LecturerId).NotNull().WithMessage("Lecturer id can not be empty.").GreaterThan(0).WithMessage("Lecturer id must be greater than 0.");
        }
    }
}
