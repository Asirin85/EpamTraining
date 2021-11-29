using FluentValidation;
using RestApi.Models;

namespace RestApi.Validation
{
    internal class HomeworkValidator : AbstractValidator<HomeworkValid>
    {
        public HomeworkValidator()
        {
            RuleFor(x => x.Task).NotNull().WithMessage("Homework task can not be empty.").Length(1, 300).WithMessage("Homework task must be more than 1 and less than 300 symbols.");
        }
    }
}
