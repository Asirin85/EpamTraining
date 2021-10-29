namespace RestApi.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using RestApi.Models;
    using RestApi.Validation;
    using System.Collections.Generic;

    public class LectureValidationTests
    {
        private LectureValidator _lectureValidator;
        [SetUp]
        public void Setup()
        {
            _lectureValidator = new LectureValidator();
        }
        private static List<TestCaseData> _dataForLectureErrorValidation = new()
        {
            new TestCaseData(new LectureValid(null, 1, 1)),
            new TestCaseData(new LectureValid("", 1, 1)),
            new TestCaseData(new LectureValid(new string('a', 31), 1, 1)),
            new TestCaseData(new LectureValid("abc", -1, 1)),
            new TestCaseData(new LectureValid("abc", 1, -1)),
        };
        [TestCaseSource(nameof(_dataForLectureErrorValidation))]
        public void Test_For_LectureValidator_Error(LectureValid model)
        {
            var result = _lectureValidator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }
        private static List<TestCaseData> _dataForLectureCorrectValidation = new()
        {
            new TestCaseData(new LectureValid("abc", 1, 1)),
            new TestCaseData(new LectureValid("abc", 1, null)),
        };
        [TestCaseSource(nameof(_dataForLectureCorrectValidation))]
        public void Test_For_LectureValidator_Correct(LectureValid model)
        {
            var result = _lectureValidator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
