using FluentValidation.TestHelper;
using NUnit.Framework;
using RestApi.Models;
using RestApi.Validation;
using System.Collections.Generic;

namespace RestApi.Tests
{
    public class ValidationTests
    {
        private AttendanceValidator _attendanceValidator;
        private HomeworkValidator _homeworkValidadtor;
        private LecturerValidator _lecturerValidator;
        private LectureValidator _lectureValidator;
        private StudentValidator _studentValidator;
        [SetUp]
        public void Setup()
        {
            _attendanceValidator = new AttendanceValidator();
            _homeworkValidadtor = new HomeworkValidator();
            _lecturerValidator = new LecturerValidator();
            _lectureValidator = new LectureValidator();
            _studentValidator = new StudentValidator();
        }

        // Attendance validation
        private static List<TestCaseData> _dataForAttendanceErrorValidation = new List<TestCaseData>(new[]
        {
            new TestCaseData(new AttendanceValid(1,1,-100,true)),
            new TestCaseData(new AttendanceValid(1,1,100,true)),
            new TestCaseData(new AttendanceValid(-1,1,3,true)),
            new TestCaseData(new AttendanceValid(1,-1,3,true)),
            new TestCaseData(new AttendanceValid(1,1,4,false)),
            new TestCaseData(new AttendanceValid(1,1,null,false)),
            new TestCaseData(new AttendanceValid(1,1,5,null)),
        });
        [TestCaseSource(nameof(_dataForAttendanceErrorValidation))]
        public void Test_For_AttendanceValidator_Error(AttendanceValid model)
        {
            var result = _attendanceValidator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }
        private static List<TestCaseData> _dataForAttendanceCorrectValidation = new List<TestCaseData>(new[]
        {
            new TestCaseData(new AttendanceValid(1,1,0,true)),
            new TestCaseData(new AttendanceValid(1,1,0,false)),
            new TestCaseData(new AttendanceValid(1,1,5,true)),
            new TestCaseData(new AttendanceValid(1,1,null,null)),
        });
        [TestCaseSource(nameof(_dataForAttendanceCorrectValidation))]
        public void Test_For_AttendanceValidator_Correct(AttendanceValid model)
        {
            var result = _attendanceValidator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        // Homework validation
        private static List<TestCaseData> _dataForHomeworkErrorValidation = new List<TestCaseData>(new[]
        {
            new TestCaseData(new HomeworkValid("")),
            new TestCaseData(new HomeworkValid(null)),
            new TestCaseData(new HomeworkValid(new string(' ', 301))),
        });
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

        // Lecturer validation
        private static List<TestCaseData> _dataForLecturerErrorValidation = new List<TestCaseData>(new[]
        {
            new TestCaseData(new LecturerValid(null,"a@gmail.com")),
            new TestCaseData(new LecturerValid("","a@gmail.com")),
            new TestCaseData(new LecturerValid(new string('a',76),"a@gmail.com")),
            new TestCaseData(new LecturerValid("Avan",null)),
            new TestCaseData(new LecturerValid("Avan","abc")),
            new TestCaseData(new LecturerValid("Avan","")),
            new TestCaseData(new LecturerValid("Avan",$"{new string('a',66)}@gmail.com")),
            new TestCaseData(new LecturerValid("admin","a@gmail.com")),
        });
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

        //Lecture validation
        private static List<TestCaseData> _dataForLectureErrorValidation = new List<TestCaseData>(new[]
        {
            new TestCaseData(new LectureValid(null,1,1)),
            new TestCaseData(new LectureValid("",1,1)),
            new TestCaseData(new LectureValid(new string('a',31),1,1)),
            new TestCaseData(new LectureValid("abc",-1,1)),
            new TestCaseData(new LectureValid("abc",1,-1)),
        });
        [TestCaseSource(nameof(_dataForLectureErrorValidation))]
        public void Test_For_LectureValidator_Error(LectureValid model)
        {
            var result = _lectureValidator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }
        private static List<TestCaseData> _dataForLectureCorrectValidation = new List<TestCaseData>(new[]
        {
            new TestCaseData(new LectureValid("abc",1,1)),
            new TestCaseData(new LectureValid("abc",1,null)),
        });
        [TestCaseSource(nameof(_dataForLectureCorrectValidation))]
        public void Test_For_LectureValidator_Correct(LectureValid model)
        {
            var result = _lectureValidator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
        //Student validation
        private static List<TestCaseData> _dataForStudentErrorValidation = new List<TestCaseData>(new[]
        {
            new TestCaseData(new StudentValid(null,"a@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid("","a@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid(new string('a',76),"a@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid("admin","a@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio",null, "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio","abc", "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio","", "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio",$"{new string('a',66)}@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio","iva.mal@gmail.com", "")),
            new TestCaseData(new StudentValid("Ivan Malenio","iva.mal@gmail.com", null)),
            new TestCaseData(new StudentValid("Ivan Malenio","iva.mal@gmail.com", "+755563222002222")),
            new TestCaseData(new StudentValid("Ivan Malenio","iva.mal@gmail.com", "abc")),

        });
        [TestCaseSource(nameof(_dataForStudentErrorValidation))]
        public void Test_For_StudentValidator_Error(StudentValid model)
        {
            var result = _studentValidator.TestValidate(model);
            result.ShouldHaveAnyValidationError();
        }
        private static List<TestCaseData> _dataForStudentCorrectValidation = new List<TestCaseData>(new[]
        {
            new TestCaseData(new StudentValid("Ivan Malenio", "iva.mal@gmail.com", "+75556322200")),
            new TestCaseData(new StudentValid("Ivan Malenio", "iva.mal@gmail.com", "+755563222002")),

        });
        [TestCaseSource(nameof(_dataForStudentCorrectValidation))]
        public void Test_For_StudentValidator_Correct(StudentValid model)
        {
            var result = _studentValidator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}