using AutoMapper;
using DataAccess.Entities;
using Domain;
using Domain.Entities;
using Domain.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.ReposImpl
{
    internal class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        public AttendanceRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }
        public void Delete(int studentId, int lectureId)
        {
            if (_applicationContext.Attendances.FirstOrDefault(x => x.LectureId == lectureId && x.StudentId == studentId) is AttendanceDb attendanceToDelete)
            {
                _applicationContext.Entry(attendanceToDelete).State = EntityState.Deleted;
                _applicationContext.SaveChanges();
            }
        }

        public AttendaceId Edit(Attendance attendance)
        {
            if (_applicationContext.Attendances.FirstOrDefault(x => x.LectureId == attendance.LectureId && x.StudentId == attendance.StudentId) is AttendanceDb attendanceDb)
            {
                attendanceDb.Mark = attendance.Mark;
                attendanceDb.StudentAttended = attendance.StudentAttended;
                attendanceDb.LectureId = attendance.LectureId;
                attendanceDb.StudentId = attendance.StudentId;
                _applicationContext.Entry(attendanceDb).State = EntityState.Modified;
                _applicationContext.SaveChanges();
            }
            return new AttendaceId(attendance.StudentId, attendance.LectureId);
        }

        public Attendance? Get(int studentId, int lectureId)
        {
            var attendanceDb = _applicationContext.Attendances.FirstOrDefault(x => x.StudentId == studentId && x.LectureId == lectureId);
            return _mapper.Map<Attendance?>(attendanceDb);
        }
        public IEnumerable<Attendance> GetAllByStudentName(string studentName)
        {
            var attendancesDb = from att in _applicationContext.Attendances
                                where att.Student.Name.Equals(studentName)
                                select att;
            return _mapper.Map<IReadOnlyCollection<Attendance>>(attendancesDb);
        }
        public IEnumerable<Attendance> GetAllByLectureName(string lectureName)
        {
            var attendancesDb = from att in _applicationContext.Attendances
                                where att.Lecture.Name.Equals(lectureName)
                                select att;
            return _mapper.Map<IReadOnlyCollection<Attendance>>(attendancesDb);
        }
        public IEnumerable<Attendance> GetAll()
        {
            var attendancesDb = _applicationContext.Attendances.ToList();
            return _mapper.Map<IReadOnlyCollection<Attendance>>(attendancesDb);
        }

        public AttendaceId New(Attendance attendance)
        {
            var attendaceDb = _mapper.Map<AttendanceDb>(attendance);
            var result = _applicationContext.Attendances.Add(attendaceDb);
            _applicationContext.SaveChanges();
            return new AttendaceId(result.Entity.StudentId, result.Entity.LectureId);
        }
    }
}
