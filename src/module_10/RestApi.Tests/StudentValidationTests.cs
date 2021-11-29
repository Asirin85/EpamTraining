using FluentValidation.TestHelper;
using NUnit.Framework;
using RestApi.Models;
using RestApi.Validation;
using System.Collections.Generic;

namespace RestApi.Tests
{
    public class StudentValidationTests
    {
        private StudentValidator _studentValidator;
        [SetUp]
        public void Setup()
        {
            _studentValidator = new StudentValidator();
        }
        private static List<TestCaseData> _dataForStudentErrorValidation = new()
        {
            new TestCaseData(new StudentValid(null, "a@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid("", "a@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid(new string('a', 76), "a@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid("admin", "a@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio", null, "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio", "abc", "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio", "", "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio", $"{new string('a', 66)}@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio", "iva.mal@gmail.com", "")),
            new TestCaseData(new StudentValid("Ivan Malenio", "iva.mal@gmail.com", null)),
            new TestCaseData(new StudentValid("Ivan Malenio", "iva.mal@gmail.com", "+755563222002222")),
            new TestCaseData(new StudentValid("Ivan Malenio", "iva.mal@gmail.com", "abc")),

        };
        [TestCaseSource(nameof(_dataForStudentErrorValidation))]
        public void Test_For_StudentValidator_Error(StudentValid model)
        {
            var result = _studentValidator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }
        private static List<TestCaseData> _dataForStudentCorrectValidation = new()
        {
            new TestCaseData(new StudentValid("Ivan Malenio", "iva.mal@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio", "iva.mal@gmail.com", "+755563222002")),

        };
        [TestCaseSource(nameof(_dataForStudentCorrectValidation))]
        public void Test_For_StudentValidator_Correct(StudentValid model)
        {
            var result = _studentValidator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}