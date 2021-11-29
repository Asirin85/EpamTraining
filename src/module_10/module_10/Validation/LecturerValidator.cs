using FluentValidation;
using RestApi.Models;

namespace RestApi.Validation
{
    internal class LecturerValidator : AbstractValidator<LecturerValid>
    {
        public LecturerValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Lecturer name can not be empty.").Length(1, 75).WithMessage("Lecturer name must be more than 1 and less than 75 symbols.").NotEqual("admin").WithMessage("Lecturer name can not be 'admin'.");
            RuleFor(x => x.Email).NotNull().WithMessage("Lecturer email can not be empty.").Length(1, 75).WithMessage("Lecturer email must be more than 1 and less than 75 symbols.").EmailAddress().WithMessage("Lecturer email must be in correct format.");
        }
    }
}
