using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IAttendanceService
    {
        Attendance? Get(int studentId, int lectureId);
        IReadOnlyCollection<Attendance> GetAll();
        AttendaceId New(Attendance attendance);
        AttendaceId Edit(Attendance attendance);
        void Delete(int studentId, int lectureId);
        IReadOnlyCollection<Attendance> GetAllByStudentName(string studentName);
        IReadOnlyCollection<Attendance> GetAllByLectureName(string lectureName);
    }
}
