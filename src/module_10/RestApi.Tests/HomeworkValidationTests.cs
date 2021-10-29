namespace RestApi.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using RestApi.Models;
    using RestApi.Validation;
    using System.Collections.Generic;

    public class HomeworkValidationTests
    {
        private HomeworkValidator _homeworkValidadtor;
        [SetUp]
        public void Setup()
        {
            _homeworkValidadtor = new HomeworkValidator();
        }
        private static List<TestCaseData> _dataForHomeworkErrorValidation = new()
        {
            new TestCaseData(new HomeworkValid("")),
            new TestCaseData(new HomeworkValid(null)),
            new TestCaseData(new HomeworkValid(new string(' ', 301))),
        };
        [TestCaseSource(nameof(_dataForHomeworkErrorValidation))]
        public void Test_For_HomeworkValidator_Error(HomeworkValid model)
        {
            var result = _homeworkValidadtor.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }
        [TestCase]
        public void Test_For_HomeworkValidator_Correct()
        {
            var model = new HomeworkValid("My first task");
            var result = _homeworkValidadtor.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
