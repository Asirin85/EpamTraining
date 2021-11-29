namespace RestApi.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using RestApi.Models;
    using RestApi.Validation;
    using System.Collections.Generic;

    class LecturerValidationTests
    {
        private LecturerValidator _lecturerValidator;
        [SetUp]
        public void Setup()
        {
            _lecturerValidator = new LecturerValidator();
        }
        private static List<TestCaseData> _dataForLecturerErrorValidation = new()
        {
            new TestCaseData(new LecturerValid(null, "a@gmail.com")),
            new TestCaseData(new LecturerValid("", "a@gmail.com")),
            new TestCaseData(new LecturerValid(new string('a', 76), "a@gmail.com")),
            new TestCaseData(new LecturerValid("Avan", null)),
            new TestCaseData(new LecturerValid("Avan", "abc")),
            new TestCaseData(new LecturerValid("Avan", "")),
            new TestCaseData(new LecturerValid("Avan", $"{new string('a', 66)}@gmail.com")),
            new TestCaseData(new LecturerValid("admin", "a@gmail.com")),
        };
        [TestCaseSource(nameof(_dataForLecturerErrorValidation))]
        public void Test_For_LecturerValidator_Error(LecturerValid model)
        {
            var result = _lecturerValidator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }
        [TestCase]
        public void Test_For_LecturerValidator_Correct()
        {
            var model = new LecturerValid("Avan Malenio", "ava.mal@gmail.com");
            var result = _lecturerValidator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
