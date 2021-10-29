using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Logic;
using Domain.Entities;
using Domain.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BusinessLogic.Tests
{
    public class Tests
    {
        [TestCase("any", null)]
        public void Test_For_ReportCreator_NullFormat(string lectureOrStudentName, string format)
        {
            var attendanceMock = new Mock<IAttendanceService>();
            var converterMock = new Mock<IFormatConverter<Attendance>>();
            var reportCreator = new ReportCreator(attendanceMock.Object, converterMock.Object);

            Assert.Throws<FormatNullException>(delegate { reportCreator.CreateReportByLectureName(lectureOrStudentName, format); });
            Assert.Throws<FormatNullException>(delegate { reportCreator.CreateReportByStudentName(lectureOrStudentName, format); });
        }
        [TestCase(null, "any")]
        public void Test_For_ReportCreator_NullStudentOrLectureName(string lectureOrStudentName, string format)
        {
            var attendanceMock = new Mock<IAttendanceService>();
            var converterMock = new Mock<IFormatConverter<Attendance>>();
            var reportCreator = new ReportCreator(attendanceMock.Object, converterMock.Object);

            Assert.Throws<LectureNameNullException>(delegate { reportCreator.CreateReportByLectureName(lectureOrStudentName, format); });
            Assert.Throws<StudentNameNullException>(delegate { reportCreator.CreateReportByStudentName(lectureOrStudentName, format); });
        }
        [TestCase("any", "json")]
        public void Test_For_ReportCreator_NullAttendanceList(string lectureOrStudentName, string format)
        {
            var attendanceMock = new Mock<IAttendanceService>();
            var converterMock = new Mock<IFormatConverter<Attendance>>();
            var reportCreator = new ReportCreator(attendanceMock.Object, converterMock.Object);
            attendanceMock.Setup(x => x.GetAllByLectureName(It.IsAny<string>())).Returns((IReadOnlyCollection<Attendance>)null);
            attendanceMock.Setup(x => x.GetAllByStudentName(It.IsAny<string>())).Returns((IReadOnlyCollection<Attendance>)null);

            Assert.Throws<AttendanceListNullException>(delegate { reportCreator.CreateReportByLectureName(lectureOrStudentName, format); });
            Assert.Throws<AttendanceListNullException>(delegate { reportCreator.CreateReportByStudentName(lectureOrStudentName, format); });
        }
        [TestCase("any", "any")]
        public void Test_For_ReportCreator_NotSupportedFormat(string lectureOrStudentName, string format)
        {
            var attendanceMock = new Mock<IAttendanceService>();
            var converterMock = new Mock<IFormatConverter<Attendance>>();
            var reportCreator = new ReportCreator(attendanceMock.Object, converterMock.Object);
            attendanceMock.Setup(x => x.GetAllByLectureName(It.IsAny<string>())).Returns(new List<Attendance> { new Attendance { LectureId = 1, StudentId = 1, Mark = 0, StudentAttended = false } });
            attendanceMock.Setup(x => x.GetAllByStudentName(It.IsAny<string>())).Returns(new List<Attendance> { new Attendance { LectureId = 1, StudentId = 1, Mark = 0, StudentAttended = false } });
            Assert.Throws<ReportFormatNotSupportedException>(delegate { reportCreator.CreateReportByLectureName(lectureOrStudentName, format); });
            Assert.Throws<ReportFormatNotSupportedException>(delegate { reportCreator.CreateReportByStudentName(lectureOrStudentName, format); });
        }
        [TestCase("any", "json")]
        [TestCase("any", "xml")]
        public void Test_For_ReportCreator_FormatCorrectInput(string lectureOrStudentName, string format)
        {
            var attendanceMock = new Mock<IAttendanceService>();
            var converterMock = new Mock<IFormatConverter<Attendance>>();
            var reportCreator = new ReportCreator(attendanceMock.Object, converterMock.Object);
            converterMock.Setup(x => x.ConvertToJSONString(It.IsAny<List<Attendance>>())).Returns("Ok");
            converterMock.Setup(x => x.ConvertToXMLString(It.IsAny<List<Attendance>>())).Returns("Ok");
            attendanceMock.Setup(x => x.GetAllByLectureName(It.IsAny<string>())).Returns(new List<Attendance> { new Attendance { LectureId = 1, StudentId = 1, Mark = 0, StudentAttended = false } });
            attendanceMock.Setup(x => x.GetAllByStudentName(It.IsAny<string>())).Returns(new List<Attendance> { new Attendance { LectureId = 1, StudentId = 1, Mark = 0, StudentAttended = false } });
            Assert.AreEqual(reportCreator.CreateReportByLectureName(lectureOrStudentName, format), "Ok");
            Assert.AreEqual(reportCreator.CreateReportByStudentName(lectureOrStudentName, format), "Ok");
        }
    }
}