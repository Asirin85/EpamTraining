using AutoMapper;
using DataAccess.ReposImpl;
using Domain.Entities;
using Domain.Repos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Tests
{
    [TestFixture]
    public class DataBaseIntegration
    {
        private ApplicationContext _applicationContext;
        private IAttendanceRepository _attendanceRepository;
        private IStudentRepository _studentRepository;
        private ILectureRepository _lectureRepository;
        private ILecturerRepository _lecturerRepository;
        private IHomeworkRepository _homeworkRepository;
        private IMapper _mapper;

        [OneTimeSetUp]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase("ApplicationDatabase").Options;
            _applicationContext = new ApplicationContext(options);

            Seed();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            _mapper = config.CreateMapper();

            _attendanceRepository = new AttendanceRepository(_applicationContext, _mapper);
            _studentRepository = new StudentRepository(_applicationContext, _mapper);
            _lectureRepository = new LectureRepository(_applicationContext, _mapper);
            _lecturerRepository = new LecturerRepository(_applicationContext, _mapper);
            _homeworkRepository = new HomeworkRepository(_applicationContext, _mapper);
        }
        private void Seed()
        {
            _applicationContext.Lecturers.Add(new Entities.LecturerDb { Id = 1, Name = "Bacherest", Email = "var@gmail.com" });
            _applicationContext.Students.Add(new Entities.StudentDb { Id = 1, Name = "Ivan", Email = "abc@mail.ru", PhoneNumber = "+72224124242" });
            _applicationContext.Homeworks.Add(new Entities.HomeworkDb { Id = 1, Task = "Принести спортивную форму" });
            _applicationContext.Lectures.Add(new Entities.LectureDb { Id = 1, Name = "PE", HomeworkId = 1, LecturerId = 1 });
            _applicationContext.Attendances.Add(new Entities.AttendanceDb { LectureId = 1, StudentId = 1, Mark = 3, StudentAttended = true });
            _applicationContext.SaveChanges();
        }
        [OneTimeTearDown]
        public void Cleanup()
        {
            _applicationContext.Dispose();
        }

        [Test]
        public void Test_For_LecturerRepos_New()
        {
            var lecturer = new Lecturer { Name = "Ivan", Email = "ivan@mail.ru" };
            var idNew = _lecturerRepository.New(lecturer);
            var newLecturerFromDb = _applicationContext.Lecturers.Find(idNew);
            var newLecturerConverted = _mapper.Map<Lecturer>(newLecturerFromDb);
            _applicationContext.Entry(newLecturerFromDb).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
            Assert.AreEqual(lecturer with { Id = idNew }, newLecturerConverted);
        }

        [Test]
        public void Test_For_LecturerRepos_Edit()
        {
            var lecturerEdited = new Lecturer { Id = 1, Name = "Oleg", Email = "am@gmail.com" };
            var idEdited = _lecturerRepository.Edit(lecturerEdited);
            var lecturerEditedDb = _applicationContext.Lecturers.Find(idEdited);
            var lecturerEditedConverted = _mapper.Map<Lecturer>(lecturerEditedDb);
            Assert.AreEqual(lecturerEdited, lecturerEditedConverted);
        }
        [Test]
        public void Test_For_LecturerRepos_Delete()
        {
            var idInDb = 2;
            var newLecturerDb = new Entities.LecturerDb { Id = idInDb, Name = "Lolerost", Email = "lol@gmail.com" };
            _applicationContext.Lecturers.Add(newLecturerDb);
            _applicationContext.SaveChanges();

            Assert.AreEqual(newLecturerDb, _applicationContext.Lecturers.Find(idInDb));
            _lecturerRepository.Delete(idInDb);
            Assert.AreEqual(null, _applicationContext.Lecturers.Find(idInDb));
        }
        [Test]
        public void Test_For_LecturerRepos_Get()
        {
            var lecturer = _lecturerRepository.Get(1);
            var lecturerInDb = _applicationContext.Lecturers.Find(1);
            var lecturerInDbConverted = _mapper.Map<Lecturer>(lecturerInDb);
            Assert.AreEqual(lecturerInDbConverted, lecturer);
        }
        [Test]
        public void Test_For_LecturerRepos_GetAll()
        {
            var lecturerId2 = new Entities.LecturerDb { Id = 2, Name = "Oleg", Email = "ol@mail.ru" };
            var lecturerId3 = new Entities.LecturerDb { Id = 3, Name = "Artem", Email = "ar@mail.ru" };
            _applicationContext.Lecturers.AddRange(lecturerId2, lecturerId3);
            _applicationContext.SaveChanges();

            var lecturers = _lecturerRepository.GetAll();
            foreach (var lecturer in lecturers)
            {
                var lecturerDb = _applicationContext.Lecturers.Find(lecturer.Id);
                var lecturerDbConverted = _mapper.Map<Lecturer>(lecturerDb);
                Assert.AreEqual(lecturerDbConverted, lecturer);
            }
            Assert.AreEqual(_applicationContext.Lecturers.Count(), 3);

            _applicationContext.Entry(lecturerId2).State = EntityState.Deleted;
            _applicationContext.Entry(lecturerId3).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }
        // Homework repository
        [Test]
        public void Test_For_HomeworkRepos_New()
        {
            var homework = new Homework { Task = "Do something" };
            var idNew = _homeworkRepository.New(homework);
            var newHomeworkFromDb = _applicationContext.Homeworks.Find(idNew);
            var newHomeworkConverted = _mapper.Map<Homework>(newHomeworkFromDb);
            _applicationContext.Entry(newHomeworkFromDb).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
            Assert.AreEqual(homework with { Id = idNew }, newHomeworkConverted);
        }

        [Test]
        public void Test_For_HomeworkRepos_Edit()
        {
            var homeworkEdited = new Homework { Id = 1, Task = "I dont do doing" };
            var idEdited = _homeworkRepository.Edit(homeworkEdited);
            var homeworkEditedDb = _applicationContext.Homeworks.Find(idEdited);
            var homeworkEditedConverted = _mapper.Map<Homework>(homeworkEditedDb);
            Assert.AreEqual(homeworkEdited, homeworkEditedConverted);
        }
        [Test]
        public void Test_For_HomeworkRepos_Delete()
        {
            var idInDb = 2;
            var newHomeworkDb = new Entities.HomeworkDb { Id = idInDb, Task = "DO ! ! !" };
            _applicationContext.Homeworks.Add(newHomeworkDb);
            _applicationContext.SaveChanges();

            Assert.AreEqual(newHomeworkDb, _applicationContext.Homeworks.Find(idInDb));
            _homeworkRepository.Delete(idInDb);
            Assert.AreEqual(null, _applicationContext.Homeworks.Find(idInDb));
        }
        [Test]
        public void Test_For_HomeworkRepos_Get()
        {
            var homework = _homeworkRepository.Get(1);
            var homeworkInDb = _applicationContext.Homeworks.Find(1);
            var homeworkInDbConverted = _mapper.Map<Homework>(homeworkInDb);
            Assert.AreEqual(homeworkInDbConverted, homework);
        }
        [Test]
        public void Test_For_HomeworkRepos_GetAll()
        {
            var homeworkId2 = new Entities.HomeworkDb { Id = 2, Task = "Doing?" };
            var homeworkId3 = new Entities.HomeworkDb { Id = 3, Task = "Not doing" };
            _applicationContext.Homeworks.AddRange(homeworkId2, homeworkId3);
            _applicationContext.SaveChanges();

            var homeworks = _homeworkRepository.GetAll();
            foreach (var homework in homeworks)
            {
                var homeworkDb = _applicationContext.Homeworks.Find(homework.Id);
                var homeworkDbConverted = _mapper.Map<Homework>(homeworkDb);
                Assert.AreEqual(homeworkDbConverted, homework);
            }
            Assert.AreEqual(_applicationContext.Homeworks.Count(), 3);

            _applicationContext.Entry(homeworkId2).State = EntityState.Deleted;
            _applicationContext.Entry(homeworkId3).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }
        //Student repository
        [Test]
        public void Test_For_StudentRepos_New()
        {
            var student = new Student { Name = "Misha", Email = "mi@mail.ru", PhoneNumber = "+77775553253" };
            var idNew = _studentRepository.New(student);
            var newStudentFromDb = _applicationContext.Students.Find(idNew);
            var newStudentConverted = _mapper.Map<Student>(newStudentFromDb);
            _applicationContext.Entry(newStudentFromDb).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
            Assert.AreEqual(student with { Id = idNew }, newStudentConverted);
        }

        [Test]
        public void Test_For_StudentRepos_Edit()
        {
            var studentEdited = new Student { Id = 1, Name = "Evgeniy", Email = "ev@mail.ru", PhoneNumber = "+77775552318" };
            var idEdited = _studentRepository.Edit(studentEdited);
            var studentEditedDb = _applicationContext.Students.Find(idEdited);
            var studentEditedConverted = _mapper.Map<Student>(studentEditedDb);
            Assert.AreEqual(studentEdited, studentEditedConverted);
        }
        [Test]
        public void Test_For_StudentRepos_Delete()
        {
            var idInDb = 2;
            var newStudentDb = new Entities.StudentDb { Id = idInDb, Name = "Olga", Email = "olg@mail.ru", PhoneNumber = "+77772253253" };
            _applicationContext.Students.Add(newStudentDb);
            _applicationContext.SaveChanges();

            Assert.AreEqual(newStudentDb, _applicationContext.Students.Find(idInDb));
            _studentRepository.Delete(idInDb);
            Assert.AreEqual(null, _applicationContext.Students.Find(idInDb));
        }
        [Test]
        public void Test_For_StudentRepos_Get()
        {
            var student = _studentRepository.Get(1);
            var studentInDb = _applicationContext.Students.Find(1);
            var studentInDbConverted = _mapper.Map<Student>(studentInDb);
            Assert.AreEqual(studentInDbConverted, student);
        }
        [Test]
        public void Test_For_StudentRepos_GetAll()
        {
            var studentId2 = new Entities.StudentDb { Id = 2, Name = "Ilya", Email = "ilya@mail.ru", PhoneNumber = "+79677225325" };
            var studentId3 = new Entities.StudentDb { Id = 3, Name = "Alex", Email = "alex@mail.ru", PhoneNumber = "+76212253073" };
            _applicationContext.Students.AddRange(studentId2, studentId3);
            _applicationContext.SaveChanges();

            var students = _studentRepository.GetAll();
            foreach (var student in students)
            {
                var studentDb = _applicationContext.Students.Find(student.Id);
                var studentDbConverted = _mapper.Map<Student>(studentDb);
                Assert.AreEqual(studentDbConverted, student);
            }
            Assert.AreEqual(_applicationContext.Students.Count(), 3);

            _applicationContext.Entry(studentId2).State = EntityState.Deleted;
            _applicationContext.Entry(studentId3).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }
        // Lecture repository
        [Test]
        public void Test_For_LectureRepos_New()
        {
            var lecture = new Lecture { Name = "Rus", HomeworkId = 1, LecturerId = 1 };
            var idNew = _lectureRepository.New(lecture);
            var newLectureFromDb = _applicationContext.Lectures.Find(idNew);
            var newLectureConverted = _mapper.Map<Lecture>(newLectureFromDb);
            _applicationContext.Entry(newLectureFromDb).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
            Assert.AreEqual(lecture with { Id = idNew }, newLectureConverted);
        }

        [Test]
        public void Test_For_LectureRepos_Edit()
        {
            var lectureEdited = new Lecture { Id = 1, Name = "English", HomeworkId = 1, LecturerId = 1 };
            var idEdited = _lectureRepository.Edit(lectureEdited);
            var lectureEditedDb = _applicationContext.Lectures.Find(idEdited);
            var lectureEditedConverted = _mapper.Map<Lecture>(lectureEditedDb);
            Assert.AreEqual(lectureEdited, lectureEditedConverted);
        }
        [Test]
        public void Test_For_LectureRepos_Delete()
        {
            var idInDb = 2;
            var newLectureDb = new Entities.LectureDb { Id = idInDb, Name = "Belarus", HomeworkId = 1, LecturerId = 1 };
            _applicationContext.Lectures.Add(newLectureDb);
            _applicationContext.SaveChanges();

            Assert.AreEqual(newLectureDb, _applicationContext.Lectures.Find(idInDb));
            _lectureRepository.Delete(idInDb);
            Assert.AreEqual(null, _applicationContext.Lectures.Find(idInDb));
        }
        [Test]
        public void Test_For_LectureRepos_Get()
        {
            var lecture = _lectureRepository.Get(1);
            var lectureInDb = _applicationContext.Lectures.Find(1);
            var lectureInDbConverted = _mapper.Map<Lecture>(lectureInDb);
            Assert.AreEqual(lectureInDbConverted, lecture);
        }
        [Test]
        public void Test_For_LectureRepos_GetAll()
        {
            var lectureId2 = new Entities.LectureDb { Id = 2, Name = "Math", HomeworkId = 1, LecturerId = 1 };
            var lectureId3 = new Entities.LectureDb { Id = 3, Name = "Physics", HomeworkId = 1, LecturerId = 1 };
            _applicationContext.Lectures.AddRange(lectureId2, lectureId3);
            _applicationContext.SaveChanges();

            var lectures = _lectureRepository.GetAll();
            foreach (var lecture in lectures)
            {
                var lectureDb = _applicationContext.Lectures.Find(lecture.Id);
                var lectureDbConverted = _mapper.Map<Lecture>(lectureDb);
                Assert.AreEqual(lectureDbConverted, lecture);
            }
            Assert.AreEqual(_applicationContext.Lectures.Count(), 3);

            _applicationContext.Entry(lectureId2).State = EntityState.Deleted;
            _applicationContext.Entry(lectureId3).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }
        // Attendance repository
        [Test]
        public void Test_For_AttendanceRepos_New()
        {
            var newLecture = new Entities.LectureDb { Id = 2, LecturerId = 1, HomeworkId = 1, Name = "NewLecture" };
            _applicationContext.Lectures.Add(newLecture);
            _applicationContext.SaveChanges();

            var attendance = new Attendance { LectureId = 2, StudentId = 1, Mark = 2, StudentAttended = true };
            var idNew = _attendanceRepository.New(attendance);

            var newAttendanceFromDb = _applicationContext.Attendances.FirstOrDefault(x => x.LectureId == idNew.lectureId && x.StudentId == idNew.studentId);
            var newAttendanceConverted = _mapper.Map<Attendance>(newAttendanceFromDb);
            _applicationContext.Entry(newAttendanceFromDb).State = EntityState.Deleted;
            _applicationContext.Entry(newLecture).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
            Assert.AreEqual(attendance, newAttendanceConverted);
        }

        [Test]
        public void Test_For_AttendanceRepos_Edit()
        {
            var attendanceEdited = new Attendance { LectureId = 1, StudentId = 1, Mark = 0, StudentAttended = false };
            var idEdited = _attendanceRepository.Edit(attendanceEdited);
            var attendanceEditedDb = _applicationContext.Attendances.Find(idEdited.lectureId, idEdited.studentId);
            var attendanceEditedConverted = _mapper.Map<Attendance>(attendanceEditedDb);
            Assert.AreEqual(attendanceEdited, attendanceEditedConverted);
        }
        [Test]
        public void Test_For_AttendanceRepos_GetAllByStudentName()
        {
            var student = new Entities.StudentDb { Id = 2, Name = "Anton", Email = "a@mail.ru", PhoneNumber = "+71234326262" };
            _applicationContext.Students.Add(student);
            _applicationContext.SaveChanges();
            var attendance = new Entities.AttendanceDb { LectureId = 1, StudentId = 2 };
            _applicationContext.Attendances.Add(attendance);
            _applicationContext.SaveChanges();

            var studentName = "Ivan";
            var attendancesByStudentName = _attendanceRepository.GetAllByStudentName(studentName);
            var attendancesByStudentNameDb = _applicationContext.Attendances.Where(x => x.Student.Name.Equals(studentName));
            var attendancesByStudentNameDbConverted = _mapper.Map<IEnumerable<Attendance>>(attendancesByStudentNameDb);
            Assert.AreEqual(attendancesByStudentName, attendancesByStudentNameDbConverted);

            _applicationContext.Entry(attendance).State = EntityState.Deleted;
            _applicationContext.Entry(student).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }
        [Test]
        public void Test_For_AttendanceRepos_GetAllByLectureName()
        {
            var lecture = new Entities.LectureDb { Id = 2, HomeworkId = 1, LecturerId = 1, Name = "NLecture" };
            _applicationContext.Lectures.Add(lecture);
            _applicationContext.SaveChanges();
            var attendance = new Entities.AttendanceDb { LectureId = 2, StudentId = 1 };
            _applicationContext.Attendances.Add(attendance);
            _applicationContext.SaveChanges();

            var lectureName = "PE";
            var attendancesByLectureName = _attendanceRepository.GetAllByLectureName(lectureName);
            var attendancesByLectureNameDb = _applicationContext.Attendances.Where(x => x.Lecture.Name.Equals(lectureName));
            var attendancesByLectureNameDbConverted = _mapper.Map<IEnumerable<Attendance>>(attendancesByLectureNameDb);
            Assert.AreEqual(attendancesByLectureName, attendancesByLectureNameDbConverted);

            _applicationContext.Entry(attendance).State = EntityState.Deleted;
            _applicationContext.Entry(lecture).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }
        [Test]
        public void Test_For_AttendanceRepos_Delete()
        {
            var (studentId, lectureId) = (1, 2);
            var newLecture = new Entities.LectureDb { Id = lectureId, LecturerId = 1, HomeworkId = 1, Name = "NewLecture" };
            _applicationContext.Lectures.Add(newLecture);

            var newAttendanceDb = new Entities.AttendanceDb { LectureId = lectureId, StudentId = studentId, Mark = 5, StudentAttended = true };
            _applicationContext.Attendances.Add(newAttendanceDb);
            _applicationContext.SaveChanges();

            Assert.AreEqual(newAttendanceDb, _applicationContext.Attendances.Find(studentId, lectureId));
            _attendanceRepository.Delete(studentId, lectureId);
            Assert.AreEqual(null, _applicationContext.Attendances.Find(studentId, lectureId));
            _applicationContext.Entry(newLecture).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }
        [Test]
        public void Test_For_AttendanceRepos_Get()
        {
            var attendance = _attendanceRepository.Get(1, 1);
            var attendanceInDb = _applicationContext.Attendances.Find(1, 1);
            var attendanceInDbConverted = _mapper.Map<Attendance>(attendanceInDb);
            Assert.AreEqual(attendanceInDbConverted, attendance);
        }
        [Test]
        public void Test_For_AttendanceRepos_GetAll()
        {
            var newLecture2 = new Entities.LectureDb { Id = 2, LecturerId = 1, HomeworkId = 1, Name = "NewLecture" };
            _applicationContext.Lectures.Add(newLecture2);
            var newLecture3 = new Entities.LectureDb { Id = 3, LecturerId = 1, HomeworkId = 1, Name = "NewLecture" };
            _applicationContext.Lectures.Add(newLecture3);
            _applicationContext.SaveChanges();

            var attendanceId2 = new Entities.AttendanceDb { LectureId = 2, StudentId = 1, Mark = 5, StudentAttended = true };
            var attendanceId3 = new Entities.AttendanceDb { LectureId = 3, StudentId = 1, Mark = 5, StudentAttended = true };
            _applicationContext.Attendances.AddRange(attendanceId2, attendanceId3);
            _applicationContext.SaveChanges();

            var attendances = _attendanceRepository.GetAll();
            foreach (var attendance in attendances)
            {
                var attendanceDb = _applicationContext.Attendances.Find(attendance.StudentId, attendance.LectureId);
                var attendanceDbConverted = _mapper.Map<Attendance>(attendanceDb);
                Assert.AreEqual(attendanceDbConverted, attendance);
            }
            Assert.AreEqual(_applicationContext.Attendances.Count(), 3);

            _applicationContext.Entry(attendanceId2).State = EntityState.Deleted;
            _applicationContext.Entry(attendanceId3).State = EntityState.Deleted;
            _applicationContext.Entry(newLecture2).State = EntityState.Deleted;
            _applicationContext.Entry(newLecture3).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }
    }
}