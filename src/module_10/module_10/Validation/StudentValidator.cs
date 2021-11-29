using FluentValidation;
using RestApi.Models;
using System.Text.RegularExpressions;

namespace RestApi.Validation
{
    internal class StudentValidator : AbstractValidator<StudentValid>
    {
        private static Regex _phoneRegex = new Regex(@"(\+[1-9][0-9]{10}[0-9]?)\b");
        public StudentValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Student name can not be empty.").Length(1, 75).WithMessage("Student name must be more than 1 and less than 75 symbols.").NotEqual("admin").WithMessage("Student name can not be 'admin'.");
            RuleFor(x => x.Email).NotNull().WithMessage("Student email can not be empty.").Length(1, 75).WithMessage("Student email must be more than 1 and less than 75 symbols.").EmailAddress().WithMessage("Incorrect email adress.");
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Student phone number can not be empty.").Must(x => PhoneNumber(x)).WithMessage("Phone number must be in correct format. Correct phone formats are: +12345678910 or +123456789101.");
        }
        private bool PhoneNumber(string number)
        {
            if (number == null) return false;
            return _phoneRegex.Match(number).Success;
        }
    }
}
