namespace RestApi.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using RestApi.Models;
    using RestApi.Validation;
    using System.Collections.Generic;

    public class AttendanceValidationTests
    {
        private AttendanceValidator _attendanceValidator;
        [SetUp]
        public void Setup()
        {
            _attendanceValidator = new AttendanceValidator();
        }
        private static List<TestCaseData> _dataForAttendanceErrorValidation = new()
        {
            new TestCaseData(new AttendanceValid(1, 1, -100, true)),
            new TestCaseData(new AttendanceValid(1, 1, 100, true)),
            new TestCaseData(new AttendanceValid(-1, 1, 3, true)),
            new TestCaseData(new AttendanceValid(1, -1, 3, true)),
            new TestCaseData(new AttendanceValid(1, 1, 4, false)),
            new TestCaseData(new AttendanceValid(1, 1, null, false)),
            new TestCaseData(new AttendanceValid(1, 1, 5, null)),
        };
        [TestCaseSource(nameof(_dataForAttendanceErrorValidation))]
        public void Test_For_AttendanceValidator_Error(AttendanceValid model)
        {
            var result = _attendanceValidator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }
        private static List<TestCaseData> _dataForAttendanceCorrectValidation = new()
        {
            new TestCaseData(new AttendanceValid(1, 1, 0, true)),
            new TestCaseData(new AttendanceValid(1, 1, 0, false)),
            new TestCaseData(new AttendanceValid(1, 1, 5, true)),
            new TestCaseData(new AttendanceValid(1, 1, null, null)),
        };
        [TestCaseSource(nameof(_dataForAttendanceCorrectValidation))]
        public void Test_For_AttendanceValidator_Correct(AttendanceValid model)
        {
            var result = _attendanceValidator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
