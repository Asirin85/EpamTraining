using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using Domain;
using Domain.Entities;
using Domain.Repos;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    internal class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ILectureRepository _lectureRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILecturerRepository _lecturerRepository;
        private readonly ISmtpClient _smptClient;
        private readonly ITwilioClient _twilioClient;

        public AttendanceService(IAttendanceRepository attendanceRepository, IStudentRepository studentRepository, ILectureRepository lectureRepository, ILecturerRepository lecturerRepository, ITwilioClient twilioClient, ISmtpClient smtpClient)
        {
            _attendanceRepository = attendanceRepository;
            _smptClient = smtpClient;
            _twilioClient = twilioClient;
            _lectureRepository = lectureRepository;
            _lecturerRepository = lecturerRepository;
            _studentRepository = studentRepository;
        }

        public void Delete(int studentId, int lectureId)
        {
            _attendanceRepository.Delete(studentId, lectureId);
        }

        public AttendaceId Edit(Attendance attendance)
        {
            var ids = _attendanceRepository.Edit(attendance);
            SendNotifications(attendance);
            return ids;
        }

        public Attendance? Get(int studentId, int lectureId)
        {
            return _attendanceRepository.Get(studentId, lectureId);
        }

        public IReadOnlyCollection<Attendance> GetAll()
        {
            return _attendanceRepository.GetAll().ToArray();
        }

        public AttendaceId New(Attendance attendance)
        {
            var ids = _attendanceRepository.New(attendance);
            SendNotifications(attendance);
            return ids;
        }
        public IReadOnlyCollection<Attendance> GetAllByStudentName(string studentName)
        {
            return _attendanceRepository.GetAllByStudentName(studentName).ToArray();
        }
        public IReadOnlyCollection<Attendance> GetAllByLectureName(string lectureName)
        {
            return _attendanceRepository.GetAllByLectureName(lectureName).ToArray();
        }
        public void SendNotifications(Attendance attendance)
        {
            var attendedLecture = _lectureRepository.Get(attendance.LectureId);
            if (attendedLecture is null) throw new LectureNullException($"Unable to find attended lecture with id = {attendance.LectureId}, by student with id = {attendance.StudentId}.");
            var student = _studentRepository.Get(attendance.StudentId);
            if (student is null) throw new StudentNullException($"Unable to find student with id = {attendance.StudentId}.");
            var courseLecturer = _lecturerRepository.Get(attendedLecture.LecturerId);
            if (courseLecturer is null) throw new LecturerNullException($"Unable to find lecturer with id = {attendedLecture.LecturerId}");

            var studentAttendancesByLectureName = GetAllByLectureName(attendedLecture.Name).Where(x => x.StudentId == student.Id).ToArray();
            var average = studentAttendancesByLectureName.Where(x => x.Mark is not null).Average(x => x.Mark);
            if (average is not null && average < 4)
            {
                _twilioClient.Send(student.PhoneNumber, "Your grades in the course have dropped", $"Your grades in {attendedLecture.Name} course dropped below 4, pull yourself together!");
            }
            if (attendance.StudentAttended == false)
            {
                var skippedLecturesInCourse = studentAttendancesByLectureName.Where(x => x.StudentAttended == false);

                if (skippedLecturesInCourse.Count() > 3)
                {
                    _smptClient.Send(courseLecturer.Email, $"Student {student.Name} attendance", $"{student.Name} has skipped more that 3 lectures on subject {attendedLecture.Name}");
                    _smptClient.Send(student.Email, $"Student {student.Name} attendance", $"{student.Name} has skipped more that 3 lectures on subject {attendedLecture.Name}");
                }
            }
        }
    }
}
