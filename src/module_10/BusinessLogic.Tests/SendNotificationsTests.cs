namespace BusinessLogic.Tests
{
    using BusinessLogic.Exceptions;
    using BusinessLogic.Interfaces;
    using BusinessLogic.Services;
    using Domain.Entities;
    using Domain.Repos;
    using Domain.Services;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;

    public class SendNotificationsTests
    {
        private Mock<IAttendanceRepository> _attendanceRepo;
        private Mock<IStudentRepository> _studentRepo;
        private Mock<ILectureRepository> _lectureRepo;
        private Mock<ILecturerRepository> _lecturerRepo;
        private Mock<ITwilioClient> _twilio;
        private Mock<ISmtpClient> _smtp;
        [SetUp]
        public void Setup()
        {
            _attendanceRepo = new Mock<IAttendanceRepository>();
            _studentRepo = new Mock<IStudentRepository>();
            _lectureRepo = new Mock<ILectureRepository>();
            _lecturerRepo = new Mock<ILecturerRepository>();
            _twilio = new Mock<ITwilioClient>();
            _smtp = new Mock<ISmtpClient>();
        }

        [Test]
        public void Test_For_SendNotifications_NullLecture()
        {
            var att = new Attendance { LectureId = 1, StudentId = 1, StudentAttended = true, Mark = 5 };
            var attendanceService = new AttendanceService(_attendanceRepo.Object, _studentRepo.Object, _lectureRepo.Object, _lecturerRepo.Object, _twilio.Object, _smtp.Object);
            Assert.Throws<LectureNullException>(delegate { attendanceService.SendNotifications(att); });
        }
        [Test]
        public void Test_For_SendNotifications_NullStudent()
        {
            var att = new Attendance { LectureId = 1, StudentId = 1, StudentAttended = true, Mark = 5 };
            _lectureRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Lecture { Id = 1, Name = "PE", HomeworkId = 1, LecturerId = 1 });
            var attendanceService = new AttendanceService(_attendanceRepo.Object, _studentRepo.Object, _lectureRepo.Object, _lecturerRepo.Object, _twilio.Object, _smtp.Object);
            Assert.Throws<StudentNullException>(delegate { attendanceService.SendNotifications(att); });
        }
        [Test]
        public void Test_For_SendNotifications_NullLecturer()
        {
            var att = new Attendance { LectureId = 1, StudentId = 1, StudentAttended = true, Mark = 5 };
            _lectureRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Lecture { Id = 1, Name = "PE", HomeworkId = 1, LecturerId = 1 });
            _studentRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Student { Id = 1, Name = "Ivan", Email = "ivan@mail.ru", PhoneNumber = "+77775553275" });
            var attendanceService = new AttendanceService(_attendanceRepo.Object, _studentRepo.Object, _lectureRepo.Object, _lecturerRepo.Object, _twilio.Object, _smtp.Object);
            Assert.Throws<LecturerNullException>(delegate { attendanceService.SendNotifications(att); });
        }
        [Test]
        public void Test_For_SendNotifications_SmsNotificated()
        {
            var att = new Attendance { LectureId = 1, StudentId = 1, StudentAttended = true, Mark = 2 };
            _lectureRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Lecture { Id = 1, Name = "PE", HomeworkId = 1, LecturerId = 1 });
            _studentRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Student { Id = 1, Name = "Ivan", Email = "ivan@mail.ru", PhoneNumber = "+77775553275" });
            _lecturerRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Lecturer { Id = 1, Name = "Oleg", Email = "ol@mail.ru" });
            _attendanceRepo.Setup(x => x.GetAllByLectureName(It.IsAny<string>())).Returns(new List<Attendance> { att });
            _twilio.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            var attendanceService = new AttendanceService(_attendanceRepo.Object, _studentRepo.Object, _lectureRepo.Object, _lecturerRepo.Object, _twilio.Object, _smtp.Object);
            attendanceService.SendNotifications(att);
            _twilio.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

        }
        [Test]
        public void Test_For_SendNotifications_EmailSmsNotificated()
        {
            var att = new Attendance { LectureId = 1, StudentId = 1, StudentAttended = false, Mark = 0 };
            _lectureRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Lecture { Id = 1, Name = "PE", HomeworkId = 1, LecturerId = 1 });
            _studentRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Student { Id = 1, Name = "Ivan", Email = "ivan@mail.ru", PhoneNumber = "+77775553275" });
            _lecturerRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Lecturer { Id = 1, Name = "Oleg", Email = "ol@mail.ru" });
            _attendanceRepo.Setup(x => x.GetAllByLectureName(It.IsAny<string>())).Returns(new List<Attendance> {
                att,
                new Attendance {LectureId=2, StudentId =1, StudentAttended = false, Mark = 0 },
                new Attendance {LectureId=3, StudentId =1, StudentAttended = false, Mark = 0 },
                new Attendance {LectureId=4, StudentId =1, StudentAttended = false, Mark = 0 }});
            _twilio.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            _smtp.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            var attendanceService = new AttendanceService(_attendanceRepo.Object, _studentRepo.Object, _lectureRepo.Object, _lecturerRepo.Object, _twilio.Object, _smtp.Object);
            attendanceService.SendNotifications(att);
            _twilio.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _smtp.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

        }
        [Test]
        public void Test_For_SendNotifications_EmailNotificated()
        {
            var att = new Attendance { LectureId = 1, StudentId = 1, StudentAttended = false, Mark = 0 };
            _lectureRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Lecture { Id = 1, Name = "PE", HomeworkId = 1, LecturerId = 1 });
            _studentRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Student { Id = 1, Name = "Ivan", Email = "ivan@mail.ru", PhoneNumber = "+77775553275" });
            _lecturerRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(new Lecturer { Id = 1, Name = "Oleg", Email = "ol@mail.ru" });
            _attendanceRepo.Setup(x => x.GetAllByLectureName(It.IsAny<string>())).Returns(new List<Attendance> {
                att,
                new Attendance {LectureId=2, StudentId =1, StudentAttended = false, Mark = 0 },
                new Attendance {LectureId=3, StudentId =1, StudentAttended = false, Mark = 0 },
                new Attendance {LectureId=4, StudentId =1, StudentAttended = false, Mark = 0 }});
           _smtp.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            var attendanceService = new AttendanceService(_attendanceRepo.Object, _studentRepo.Object, _lectureRepo.Object, _lecturerRepo.Object, _twilio.Object, _smtp.Object);
            attendanceService.SendNotifications(att);
            _smtp.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}